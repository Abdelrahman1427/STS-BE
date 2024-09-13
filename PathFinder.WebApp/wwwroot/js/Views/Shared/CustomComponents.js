function DrawDataTable({ baseUrl = "",
    columnsName = [],
    columnsPropertyNames = [],
    actions = [{ title: "Edit", modalId: "update-modal", type: "ajax-modal", icon: 'fa-pencil', actionName: "update", ownerColumnName: 'name' }
        , { title: "Delete", modalId: "delete-modal", type: "form-modal", icon: 'fa-trash-o', actionName: "delete", ownerColumnName: 'name' }
        , { title: "Accept Account", modalId: "acceptAccount-modal", type: "form-modal", icon: 'fa-thumbs-up', actionName: "AcceptStatus", ownerColumnName: 'name' }
        , { title: "Reject Status", modalId: "rejectAccount-modal", type: "form-modal", icon: 'fa-thumbs-up', actionName: "RejectStatus", ownerColumnName: 'name' }],
    customData = {},//to send data for other page ex: in exchange rate page we need to has data called currencyId
    loadTableActionName = 'LoadDataTable',
    specialDesignColumns = [], // custom Design
    columnsIsOrder = true, //All columns has order or not
    HasLengthChange = true,// Droup down of lenght page
    ShowInfoPage = true, //Show page size and Info
    hasToolTip = {},
    fixedColumns = null
    }) {
    let lang = $('table').attr('lang');
    let columns = CreateTableColumns(baseUrl, columnsName, columnsPropertyNames, actions, lang, specialDesignColumns, columnsIsOrder, hasToolTip)
    var table;
     
    if ($.fn.dataTable.isDataTable('.datatable')) {
        table = $('.datatable').DataTable();
        table.ajax.reload();
        //$(`#${TableName}`).DataTable().columns.adjust();
    }
    else {
         
        table = $('.datatable').DataTable({
            "language": {
                "sLengthMenu": lang == 'en-US' ? "Show _MENU_ entries" : "أظهر _MENU_ مدخلات",
                "sZeroRecords": lang == 'en-US' ? "No data available" : "لم يعثر على أية سجلات",
                "sInfo": lang == 'en-US' ? "Showing _START_ to _END_ of _TOTAL_ entries" : "إظهار _START_ إلى _END_ من أصل _TOTAL_ مدخل",
                "sInfoEmpty": lang == 'en-US' ? "Showing 0 to 0 of 0 entries" : "يعرض 0 إلى 0 من أصل 0 سجل",
                "sSearch": lang == 'en-US' ? 'Search' : "ابحث:",
                "oPaginate": {
                    "sFirst": lang == 'en-US' ? 'First' : "الأول",
                    "sPrevious": lang == 'en-US' ? 'Previous' : "السابق",
                    "sNext": lang == 'en-US' ? 'Next' : "التالي",
                    "sLast": lang == 'en-US' ? 'Last' : "الأخير"
                }
            },
            "responsive": true,
            "serverSide": true, //for process server side
            "filter": true,
            "searching": true,
            'sDom': 'ltipr',// "lftipr" 'l' - Length changing 'f' - Filtering input 't' - The table! 'i' - Information 'p' - Pagination 'r' - pRocessing
            "orderMulti": false,
            "ajax": {
                "url": `/${baseUrl}/${loadTableActionName}`,
                'method': 'POST',
                "datatype": "json",
                "data": customData
            },
            "lengthMenu":[25,50,100],
            "info": ShowInfoPage,
            "bLengthChange": HasLengthChange,
            "ordering": columnsIsOrder,
            "fixedColumns": fixedColumns,
            "paging": true,
            "columns": columns,
            
        });
    }
    return table
}
function CreateTableColumns(baseUrl, columnsName, columnsPropertyNames, actions, lang, specialDesignColumns, columnsIsOrder, hasToolTip) {
    let columnsList = [];
    //this part to draw data as obj
    ///ex:{'data':ColumnName}
    let renderList = [];
    let isHasTooltip = false;
    if (hasToolTip.ColumnName != undefined) {
        isHasTooltip = true;
    }
    for (var index = 0; index < columnsName.length; index++) {
        if (columnsPropertyNames[index] === 'id') {
            continue;
        }
        
        else {
            let hasSpecialDesignColumns = specialDesignColumns.filter(x => x.columnIndex == index);
            if (specialDesignColumns != null && specialDesignColumns.length > 0 && hasSpecialDesignColumns.length > 0) {
                for (let item = 0; item < specialDesignColumns.length; item++) {
                    if (specialDesignColumns[item].columnIndex == index && specialDesignColumns[item].design == 'has-url') {
                        columnsList.push({
                            "data": columnsPropertyNames[index],
                            "name": columnsPropertyNames[index][0].toUpperCase() + columnsPropertyNames[index].slice(1),
                            "title": columnsName[index],
                            'render': function (data, type, row) {
                                let rowId = specialDesignColumns[item].urlId;
                                let urlHasId = rowId == undefined ? '' : row[rowId];

                                if (specialDesignColumns[item].data != undefined && specialDesignColumns[item].data != null)
                                    data = specialDesignColumns[item].data;

                                if (urlHasId == 'System' || urlHasId == undefined || urlHasId == null)
                                    return `${data == 'System' ? 'System' : ''}`;

                                return `<a href='${specialDesignColumns[item].url}${urlHasId}' class='dt-anchor'>${data}</a>`;
                               
                            }
                        });
                    }
                    else if (specialDesignColumns[item].columnIndex == index && specialDesignColumns[item].design == 'has-url-no-order') {
                        columnsList.push({
                            "data": columnsPropertyNames[index],
                            "name": columnsPropertyNames[index][0].toUpperCase() + columnsPropertyNames[index].slice(1),
                            "title": columnsName[index],
                            "orderable": false,
                            'render': function (data, type, row) {
                                let rowId = specialDesignColumns[item].urlId;
                                let urlHasId = rowId == undefined ? '' : row[rowId];

                                if (specialDesignColumns[item].data != undefined && specialDesignColumns[item].data != null)
                                    data = specialDesignColumns[item].data;

                                if (urlHasId == 'System' || urlHasId == undefined || urlHasId == null)
                                    return `${data == 'System' ? 'System' : ''}`;

                                return `<a href='${specialDesignColumns[item].url}${urlHasId}' class='dt-anchor'>${data}</a>`;

                            }
                        });
                    }

                    else if (specialDesignColumns[item].columnIndex == index && specialDesignColumns[item].design == 'photo') {
                        columnsList.push({
                            "data": columnsPropertyNames[index],
                            "name": columnsPropertyNames[index][0].toUpperCase() + columnsPropertyNames[index].slice(1),
                            "title": columnsName[index],
                             'orderable': false,
                            'render': function (data, type, row) {
                                let photoURL = specialDesignColumns[item].baseURL;
                                let columnName = specialDesignColumns[item].columnName;
                                photoURL = photoURL == null || row[columnName] == null ? "/img/user.jpg" : `${photoURL}${row[columnName]}`;

                                return `<h2 class="table-avatar"><a href="javascript:void(0);" class="avatar"><img alt="" src="${photoURL}"></a></h2>`;
                            }
                        });
                    }

                    else if (specialDesignColumns[item].columnIndex == index && specialDesignColumns[item].design == 'photo-with-view') {
                        columnsList.push({
                            "data": columnsPropertyNames[index],
                            "name": columnsPropertyNames[index][0].toUpperCase() + columnsPropertyNames[index].slice(1),
                            "title": columnsName[index],
                            'orderable': false,
                            'render': function (data, type, row) {
                                let photoURL = specialDesignColumns[item].baseURL;
                                let columnName = specialDesignColumns[item].columnName;

                                let viewUrlId = specialDesignColumns[item].viewUrlId == undefined ? row['id'] : specialDesignColumns[item].viewUrlId;
                                let viewUrl = `${specialDesignColumns[item].viewUrl}${row[viewUrlId]}`;

                                photoURL = photoURL == null || row[columnName] == null ? "/img/user.jpg" : `${photoURL}${row[columnName]}`;

                                return `<h2 class="table-avatar"><a href='${viewUrl}' class="avatar"><img alt="" src="${photoURL}"></a></h2>`;
                            }
                        });
                    }

                    else if (specialDesignColumns[item].columnIndex == index && specialDesignColumns[item].design == 'take-action') {
                        columnsList.push({
                            "data": columnsPropertyNames[index],
                            "name": columnsPropertyNames[index][0].toUpperCase() + columnsPropertyNames[index].slice(1),
                            "title": columnsName[index],
                            'orderable': false,
                            "donNotOrderable": false,
                            'render': function (data, type, row) {
                                let columnCheckName = specialDesignColumns[item].columnCheckName;
                                let yesName = specialDesignColumns[item].yesName;
                                let noName = specialDesignColumns[item].noName;

                                return `<a class="btn btn-white btn-sm btn-rounded ${specialDesignColumns[item].modalType}"  success-function-name="${specialDesignColumns[item].successFunctionName}" success-function-param="${specialDesignColumns[item].successFunctionParam}" modal-owner="${row[specialDesignColumns[item].ownerColumnName]}" modal-id="${specialDesignColumns[item].modalId}" action-url="${specialDesignColumns[item].url}?id=${row['id']}" href="#"><i class="fa fa-dot-circle-o ${row[columnCheckName] == true ? 'text-danger' : 'text-success'}"></i> ${row[columnCheckName] == true ? yesName : noName}</a>`;
                            }
                        });
                    }
                    else if (specialDesignColumns[item].columnIndex == index && specialDesignColumns[item].design == 'attendance-status') {
                        columnsList.push({
                            "data": columnsPropertyNames[index],
                            "name": columnsPropertyNames[index][0].toUpperCase(),
                            "title": columnsName[index],
                            'render': function (data, type, row, meta) {

                                let icon = '';
                                let iconName = '';
                                    switch (data) {
                                        case 1:
                                            icon = 'fa fa-sign-in';
                                            iconName = 'Sign in';
                                            break;
                                        case 2:
                                            icon = 'fa fa-sign-out';
                                            iconName = 'Sign out';
                                            break;
                                        case 3:
                                            icon = 'fa fa-coffee';
                                            iconName = 'Break in';
                                            break;
                                        case 4:
                                            icon = 'fa fa-clock-o';
                                            iconName = 'Break out';
                                            break;
                                        case 5:
                                            icon = 'fa fa-handshake-o';
                                            iconName = 'Visit start';
                                            break;
                                        case 6:
                                            icon = 'fa fa-briefcase';
                                            iconName = 'Visit end';
                                            break;
                                }
                                let drawTooltip = DrawTooltip(iconName, true); 
                                return `<div ${drawTooltip}><i class='${icon} font-size-35'></i></div>`;

                            }
                        });
                    }


                    //News
                    else if (specialDesignColumns[item].columnIndex == index && specialDesignColumns[item].design == 'is-pinned') {
                        columnsList.push({
                            "data": columnsPropertyNames[index],
                            "name": columnsPropertyNames[index].charAt(0).toUpperCase() + columnsPropertyNames[index].slice(1),
                            "title": columnsName[index],
                            'render': function (data, type, row, meta) {
                                if (data == true)
                                    return `<i class='fa fa-check-circle text-success font-size-35'></i>`;
                                else
                                    return `<i class='fa fa-times-circle text-muted font-size-35'></i>`;
                            }
                        });
                    }

                    //News
                    else if (specialDesignColumns[item].columnIndex == index && specialDesignColumns[item].design == 'show-more-btn') {
                        columnsList.push({
                            "data": columnsPropertyNames[index],
                            "name": columnsPropertyNames[index].charAt(0).toUpperCase() + columnsPropertyNames[index].slice(1),
                            "title": columnsName[index],
                            'render': function (data, type, row, meta) {
                                let modalId = specialDesignColumns[item].modalId;
                                let modalType = specialDesignColumns[item].type;
                                let modalLink = specialDesignColumns[item].url;
                                let actionName = specialDesignColumns[item].actionName;
                                let regex = /(<([^>]+)>|&nbsp;)/ig
                                let body = data;
                                if (body != null) {
                                    let result = body.replace(regex, " ");

                                    if (result.length > 40)
                                        return `<div>${result.substring(0, 40)} ... <a class="card-link pointer-hand ${modalType}" modal-id="${modalId}" action-url="${modalLink}${row.id}">${actionName}</a></div>`;
                                    else
                                        return `<div>${result}</div>`;
                                }
                                else {
                                    return `<div> </div>`;
                                }
                                
                            }
                        });
                    }

                    else if (specialDesignColumns[item].columnIndex == index && specialDesignColumns[item].design == 'linkURL') {
                        columnsList.push({
                            "data": columnsPropertyNames[index],
                            "name": columnsPropertyNames[index][0].toUpperCase() + columnsPropertyNames[index].slice(1),
                            "title": specialDesignColumns[item].columnName,
                            'render': function (data, type, row) {
                                let rowId = specialDesignColumns[item].urlId;
                                let urlHasId = rowId == undefined ? '' : row[rowId];
                                return `<div class='row' style='margin-right: 30%;'>
                                <div class='col-6'>
                                    <span>${data}</span>
                                </div>
                                <div class='col-6'>
                                    <span><a href='${specialDesignColumns[item].url}?id=${urlHasId}' class='link-primary'>${row[specialDesignColumns[item].additionRowData]} ${specialDesignColumns[item].additionTitle}</a> </span>
                                </div>
                           </div>`;
                            }
                        });
                    }

                    else if (specialDesignColumns[item].columnIndex == index && specialDesignColumns[item].design == 'remove-order') {
                        columnsList.push({
                            "data": columnsPropertyNames[index],
                            "name": columnsPropertyNames[index][0].toUpperCase() + columnsPropertyNames[index].slice(1),
                            "title": columnsName[index],
                            'orderable': false,
                        });
                    }

                    else if (specialDesignColumns[item].columnIndex == index && specialDesignColumns[item].design == 'date-time')
                    {
                        columnsList.push
                        ({
                            "data": columnsPropertyNames[index],
                            "name": columnsPropertyNames[index][0].toUpperCase() + columnsPropertyNames[index].slice(1),
                            "title": columnsName[index],
                            'render': function (data, type, row)
                            {
                                let drawTolTip = "";
                                if (isHasTooltip && row[hasToolTip.ColumnName] != null)
                                    drawTolTip = DrawTooltip(row[hasToolTip.ColumnName], hasToolTip.isSummerNoteDesign);
                                if (data != null)
                                {
                                    let date = data;
                                    if (specialDesignColumns[item].notNeedConvertTimeLocal != undefined &&
                                        specialDesignColumns[item].notNeedConvertTimeLocal == true &&
                                        specialDesignColumns[item].hasSpecialDateTime != undefined &&
                                        specialDesignColumns[item].hasSpecialDateTime == true)
                                    {
                                        switch (specialDesignColumns[item].timePattern)
                                        {
                                            case "hh:mm":
                                                date = moment(date).utc().format('HH:mm');
                                                break;

                                            case "MM-DD-YYYY":
                                                date = moment.utc(date).format("MM-DD-YYYY");
                                        }
                                        return `<div ${drawTolTip}>${date}</div>`;
                                    }
                                    else if (specialDesignColumns[item].notNeedConvertTimeLocal != undefined &&
                                             specialDesignColumns[item].notNeedConvertTimeLocal == false &&
                                             specialDesignColumns[item].hasSpecialDateTime != undefined &&
                                             specialDesignColumns[item].hasSpecialDateTime == false)
                                    {
                                        let timeZone = Intl.DateTimeFormat().resolvedOptions().timeZone;
                                        date = convertTZ(data, timeZone);
                                        switch (specialDesignColumns[item].timePattern)
                                        {
                                                case "hh:mm":
                                                    date = moment(date).format('HH:mm');
                                                    break;
                                            case "MM-DD-YYYY":
                                                date = moment(date).format('MM-DD-YYYY');
                                                     break;
                                        }
                                        return `<div ${drawTolTip}>${date}</div>`;
                                    }
                                    
                                    else
                                    {
                                        let timeZone = Intl.DateTimeFormat().resolvedOptions().timeZone;
                                        date = convertTZ(data, timeZone);
                                        // Get the day, month, and year from the date object
                                        const day = date.getDate().toString().padStart(2, '0');
                                        const month = (date.getMonth() + 1).toString().padStart(2, '0');
                                        const year = date.getFullYear().toString().slice(-2);

                                        // Get the hours, minutes, and seconds from the date object
                                        const hours = date.getHours().toString().padStart(2, '0');
                                        const minutes = date.getMinutes().toString().padStart(2, '0');
                                        const seconds = date.getSeconds().toString().padStart(2, '0');

                                        if (specialDesignColumns[item].timePattern === "timeConvertedToTimezone") {
                                            const finalStrTime = `${hours}:${minutes}`;
                                            return finalStrTime;
                                        }

                                        // Combine the date and time parts to create the final string
                                        const finalStr = `${day}-${month}-${year} ${hours}:${minutes}`;
                                        return `<div ${drawTolTip}>${finalStr}</div>`;
                                    }
                                }
                                return "";
                            }
                        });
                    }
                    else if (specialDesignColumns[item].columnIndex == index && specialDesignColumns[item].design == 'date-time-no-order') {
                        columnsList.push
                            ({
                                "data": columnsPropertyNames[index],
                                "name": columnsPropertyNames[index][0].toUpperCase() + columnsPropertyNames[index].slice(1),
                                "title": columnsName[index],
                                "orderable": false,
                                'render': function (data, type, row) {
                                    let drawTolTip = "";
                                    if (isHasTooltip && row[hasToolTip.ColumnName] != null)
                                        drawTolTip = DrawTooltip(row[hasToolTip.ColumnName], hasToolTip.isSummerNoteDesign);
                                    if (data != null) {
                                        let date = data;
                                        if (specialDesignColumns[item].notNeedConvertTimeLocal != undefined &&
                                            specialDesignColumns[item].notNeedConvertTimeLocal == true &&
                                            specialDesignColumns[item].hasSpecialDateTime != undefined &&
                                            specialDesignColumns[item].hasSpecialDateTime == true) {
                                            switch (specialDesignColumns[item].timePattern) {
                                                case "hh:mm":
                                                    date = moment(date).utc().format('HH:mm');
                                                    break;
                                            }
                                            return `<div ${drawTolTip}>${date}</div>`;
                                        }
                                        else if (specialDesignColumns[item].notNeedConvertTimeLocal != undefined &&
                                            specialDesignColumns[item].notNeedConvertTimeLocal == false &&
                                            specialDesignColumns[item].hasSpecialDateTime != undefined &&
                                            specialDesignColumns[item].hasSpecialDateTime == false) {
                                            let timeZone = Intl.DateTimeFormat().resolvedOptions().timeZone;
                                            date = convertTZ(data, timeZone);
                                            switch (specialDesignColumns[item].timePattern) {
                                                case "hh:mm":
                                                    date = moment(date).format('HH:mm');
                                                    break;
                                                case "MM-DD-YYYY":
                                                    date = moment(date).format('MM-DD-YYYY');
                                                    break;
                                            }
                                            return `<div ${drawTolTip}>${date}</div>`;
                                        }

                                        else {
                                            let timeZone = Intl.DateTimeFormat().resolvedOptions().timeZone;
                                            date = convertTZ(data, timeZone);
                                            // Get the day, month, and year from the date object
                                            const day = date.getDate().toString().padStart(2, '0');
                                            const month = (date.getMonth() + 1).toString().padStart(2, '0');
                                            const year = date.getFullYear().toString().slice(-2);

                                            // Get the hours, minutes, and seconds from the date object
                                            const hours = date.getHours().toString().padStart(2, '0');
                                            const minutes = date.getMinutes().toString().padStart(2, '0');
                                            const seconds = date.getSeconds().toString().padStart(2, '0');

                                            if (specialDesignColumns[item].timePattern === "timeConvertedToTimezone") {
                                                const finalStrTime = `${hours}:${minutes}`;
                                                return finalStrTime;
                                            }

                                            // Combine the date and time parts to create the final string
                                            const finalStr = `${day}-${month}-${year} ${hours}:${minutes}`;
                                            return `<div ${drawTolTip}>${finalStr}</div>`;
                                        }
                                    }
                                    return "";
                                }
                            });
                    }

                    //This is used when you have a DTO which have two properties {Date, Unit}
                    //to render dates in different manners such as hourly requests must be converted to time zone and daily do not 
                    // so we use unit to defer them
                    else if (specialDesignColumns[item].columnIndex == index && specialDesignColumns[item].design == 'hourlyConverted-dailyNot') {
                        columnsList.push
                            ({
                                "data": columnsPropertyNames[index],
                                "name": columnsPropertyNames[index][0].toUpperCase() + columnsPropertyNames[index].slice(1),
                                "title": columnsName[index],
                                'render': function (data, type, row) {
                                    let drawTolTip = "";
                                    if (isHasTooltip && row[hasToolTip.ColumnName] != null)
                                        drawTolTip = DrawTooltip(row[hasToolTip.ColumnName], hasToolTip.isSummerNoteDesign);
                                    if (data != null) {

                                        if (data.unit == 1) {
                                            let date = data.date;
                                            var formattedDate = moment.utc(date).format("MM-DD-YYYY");
                                            return `<div ${drawTolTip}>${formattedDate}</div>`;
                                        }
                                        else if (data.unit == 2) {

                                            let timeZone = Intl.DateTimeFormat().resolvedOptions().timeZone;
                                            date = convertTZ(data.date, timeZone);
                                            // Get the day, month, and year from the date object
                                            const day = date.getDate().toString().padStart(2, '0');
                                            const month = (date.getMonth() + 1).toString().padStart(2, '0');
                                            const year = date.getFullYear().toString().slice(-2);

                                            // Get the hours, minutes, and seconds from the date object
                                            const hours = date.getHours().toString().padStart(2, '0');
                                            const minutes = date.getMinutes().toString().padStart(2, '0');
                                            const seconds = date.getSeconds().toString().padStart(2, '0');

                                            if (specialDesignColumns[item].timePattern === "timeConvertedToTimezone") {
                                                const finalStrTime = `${hours}:${minutes}`;
                                                return finalStrTime;
                                            }

                                            // Combine the date and time parts to create the final string
                                            const finalStr = `${day}-${month}-${year} ${hours}:${minutes}`;
                                            return `<div ${drawTolTip}>${finalStr}</div>`;
                                        }
                                    }
                                    return "";
                                }
                            });
                    }

                    else if (specialDesignColumns[item].columnIndex == index && specialDesignColumns[item].design == 'net-adjust-balance')
                    {
                        columnsList.push({
                            "data": columnsPropertyNames[index],
                            "name": columnsPropertyNames[index][0].toUpperCase() + columnsPropertyNames[index].slice(1),
                            "title": columnsName[index],
                            "orderable": false,
                            'render': function (data, type, row) {
                                if (data > 0)
                                    return `<i class="fa fa-plus text-success" aria-hidden="true"></i> ${Math.abs(data)}`
                                if (data < 0)
                                    return `<i class="fa fa-minus text-danger" aria-hidden="true"></i> ${Math.abs(data)}`
                                if (data == 0)
                                    return `${data}`
                            }
                        })
                    }

                    else if (specialDesignColumns[item].columnIndex == index && specialDesignColumns[item].design == 'split') {
                        columnsList.push({
                            "data": columnsPropertyNames[index],
                            "name": columnsPropertyNames[index][0].toUpperCase() + columnsPropertyNames[index].slice(1),
                            "title": columnsName[index],
                            "orderable": false,
                            'render': function (data, type, row) {
                                let getSplitData = specialDesignColumns[item].splitBy;
                                let splitData = data.split(getSplitData);
                                let lastData = splitData[splitData.length - 1]
                                return lastData;
                            }
                        })
                    }
                    //else if (specialDesignColumns[item].design == 'timesheet-weeks-filter') {
                    //    columnsList.push({
                    //        "data": columnsPropertyNames[index],
                    //        "name": columnsPropertyNames[index][0].toUpperCase() + columnsPropertyNames[index].slice(1),
                    //        "title": columnsName[index],
                    //        "orderable": false,
                    //        'render': function (data, type, row) {
                    //            if (data > 0)
                    //                return `<i class="fa fa-plus text-success" aria-hidden="true"></i> ${Math.abs(data)}`
                    //            if (data < 0)
                    //                return `<i class="fa fa-minus text-danger" aria-hidden="true"></i> ${Math.abs(data)}`
                    //            if (data == 0)
                    //                return `${data}`
                    //        }
                    //    })
                    //}


                }
            }
            else
                columnsList.push({
                    "data": columnsPropertyNames[index],
                    "name": columnsPropertyNames[index][0].toUpperCase() + columnsPropertyNames[index].slice(1),
                    "title": columnsName[index],
                    'orderable': columnsIsOrder,
                    'render': function (data, type, row) {
                        let drawTolTip = "";
                        if (isHasTooltip && row[hasToolTip.ColumnName] != null)
                            drawTolTip = DrawTooltip(row[hasToolTip.ColumnName], hasToolTip.isSummerNoteDesign);
                        if (data == null)
                            data = '';
                        return `<div ${drawTolTip}>${data}</div>`;
                    }
                });
        }
    }
    //this part to draw Actions
    if (actions.length > 0) {
        columnsList.push({
            "data": null,
            orderable: false,
            'render': function (data, type, row) {
                let drawActions = [];
                for (let action of actions) {
                    if (action.type == "href")
                        drawActions.push(`<a class="dropdown-item ${action.type}" href="/${baseUrl}/${action.actionName}?id=${data['id']}"><i class="fa ${action.icon} m-r-5" ></i> ${action.title}</a>`)
                    else if (action.type == "fullUrl") {
                        let isUnpaid = data[`isUnpaid`];
                        if (isUnpaid)
                            drawActions.push(`<a class="dropdown-item text-muted"><i class="fa ${action.icon} m-r-5" ></i> ${action.title}</a>`)
                        else
                            drawActions.push(`<a class="dropdown-item ${action.type}" href="${action.fullUrl}?id=${data['id']}"><i class="fa ${action.icon} m-r-5" ></i> ${action.title}</a>`)
                    }
                    else {
                        //message on action for every rows
                        if (action.hasToolTip != undefined && action.hasToolTip &&
                            action.toolTipName != undefined && action.toolTipName &&
                            action.activeActionName != undefined && action.activeActionName) {
                            let CanOpenModal = data[`${action.activeActionName}`]
                            if (action.type == "fullUrlWithToolTip" && CanOpenModal)
                                drawActions.push(`<a class="dropdown-item  ${action.type}" data-bs-toggle="tooltip" title="${data[`${action.toolTipName}`]}" href="${action.fullUrl}?id=${data['id']}"><i class="fa ${action.icon} m-r-5" ></i> ${action.title}</a>`)
                            else if (action.type == "fullUrlWithToolTip" && !CanOpenModal)
                                drawActions.push(`<a class="dropdown-item text-muted" ${action.type}" data-bs-toggle="tooltip" title="${data[`${action.toolTipName}`]}" ><i class="fa ${action.icon} m-r-5" ></i> ${action.title}</a>`)
                            else if (CanOpenModal)
                                drawActions.push(`<button  data-bs-toggle="tooltip" title="${data[`${action.toolTipName}`]}"  class="dropdown-item ${action.type}" modal-owner="${data[`${action.ownerColumnName}`]}" modal-id="${action.modalId}" action-url="/${baseUrl}/${action.actionName}?id=${data['id']}"><i class="fa ${action.icon} m-r-5" ></i> ${action.title}</button>`);
                            else
                                drawActions.push(`<button  data-bs-toggle="tooltip" title="${data[`${action.toolTipName}`]}"  class="dropdown-item text-muted" modal-owner="${data[`${action.ownerColumnName}`]}" modal-id="${action.modalId}" action-url="/${baseUrl}/${action.actionName}?id=${data['id']}"><i class="fa ${action.icon} m-r-5" ></i> ${action.title}</button>`);
                        }

                        //message on action for all rows
                        else if (action.hasToolTip != undefined && action.hasToolTip == 'true' &&
                            action.toolTipMessage != undefined && action.toolTipMessage) {
                            drawActions.push(`<button  data-bs-toggle="tooltip" title="${action.toolTipMessage}"  class="dropdown-item text-muted" modal-owner="${data[`${action.ownerColumnName}`]}" modal-id="${action.modalId}" action-url="/${baseUrl}/${action.actionName}?id=${data['id']}"><i class="fa ${action.icon} m-r-5" ></i> ${action.title}</button>`);
                        } 
                        else if (action.hasClass != undefined && action.columnName != undefined && action.value != undefined) {
                            let hasClass = `${data[`${action.columnName}`]}` == `${action.value}` ? action.hasClass : '';
                            drawActions.push(`<button class="dropdown-item ${hasClass} ${action.type}" modal-owner="${data[`${action.ownerColumnName}`]}" modal-id="${action.modalId}" action-url="/${baseUrl}/${action.actionName}?id=${data['id']}"><i class="fa ${action.icon} m-r-5" ></i> ${action.title}</button>`); 
                        }

                        else
                            drawActions.push(`<button class="dropdown-item ${action.type}" modal-owner="${data[`${action.ownerColumnName}`]}" modal-id="${action.modalId}" action-url="/${baseUrl}/${action.actionName}?id=${data['id']}"><i class="fa ${action.icon} m-r-5" ></i> ${action.title}</button>`); 
                    }
                        
                }

                return `<div class="text-end">
                            <div class="dropdown dropdown-action">
                                <a href="#" class="action-icon dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">
                                        <i class="fa fa-ellipsis-v" style="padding: 5px;"></i>
                                </a>
                                <div class="dropdown-menu dropdown-menu-right">
                                    ${drawActions.join('')}
                                 </div>
                            </div>
                        </div>`
            }
        });
    }

    return columnsList;
}

function DrawTooltip(data, isSummerNoteDesign) {
    let toolTip = "";
    if (data != null && data != "") {
        if (isSummerNoteDesign)
            data = extractContent(data);
        toolTip = `data-bs-toggle="tooltip" title="${data}"`;
    }
    return toolTip;
}
function extractContent(data) {
    var span = document.createElement('span');
    span.innerHTML = data;
    return span.textContent || span.innerText;
};

function convertTZ(date, tzString) {
    let data = new Date((typeof date === "string" ? new Date(date) : date).toLocaleString("en-US", { timeZone: tzString }));
    return data;
}
function OnlyDecimalNumbers(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

function OnlyNumbers(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode < 48 || charCode > 57)
        return false;
    return true;
}

function AppendTimeZoneToDate(date) {

    let timezone = moment().format('Z');
    let modifiedDate = `${date}${timezone}`;

    return modifiedDate;
}

$(document).on('click', ".submit-modal", function (e) {
    e.preventDefault();
    let element = $(this);
    let modal = $(element).closest('.modal');
    let form = $(modal).find("form");
    let validators = form.validate();
    let isValid = form.valid();

    let data = new FormData(form[0]);
    let clientDateInputs = $(modal).find(".client-dateTime");
    if (clientDateInputs != undefined)
    {
        clientDateInputs.each(function () {
            data.set($(this).attr('name'), AppendTimeZoneToDate($(this).val()));
        })
    }
    //if (isValid) {
        $(element).prop('disabled', true);
        $.ajax({
            type: "POST",
            url: $(modal).attr('action-url'),
            processData: false,
            contentType: false,
            data: data,
            success: function (data) {
                $(element).prop('disabled', false);
                if (data.isSuccess) {
                    $(form).trigger("reset");
                    //TODO: reset form validators
                    $(modal).modal('hide');
                    let hasClass = $('#alert').hasClass("view");
                    if (hasClass) {
                        if (data.hasMessage != undefined && data.hasMessage != null) {
                            let classAlert = data.isSuccess ? 'alert-success' : 'alert-danager';
                            $('#alert')
                                .html(`<div class="alert alert-dismissible ${classAlert} show fade" role="alert">
                                        <strong>${data.message}</strong>
                                        <span type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close">
                                        </span>
                                    </div>`);
                        }
                           
                        //let path = window.location.href;
                        //let message = data.message;
                        else
                            location.reload();
                    }
                    else {
                        $('#alert')
                            .html(`<div class="alert alert-dismissible alert-success show fade" role="alert">
                                        <strong>${data.message}</strong>
                                        <span type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close">
                                        </span>
                                    </div>`);
                        if ($('.grid-table').length > 0 && $('.grid-table').css('display') != 'none') {
                            let functionName = $('.grid-table').attr('function-name');
                            window[functionName].call();
                        }
                        else {
                            $('.datatable').DataTable().ajax.reload();
                        }

                        if ($(modal).attr("success-function-name") != undefined) {
                            let param = $(modal).attr("success-function-param");
                            var sucessFunction = `${$(modal).attr("success-function-name")}(${param == undefined ? '' : `'${param}'`})`;
                            var ret = eval(sucessFunction);
                        }
                        //$.toast({
                        //    heading: "Done",
                        //    text: data.message,
                        //    bgColor: '#d1e7dd',              // Background color for toast
                        //    textColor: '#0f5132',            // text color
                        //    allowToastClose: true,       // Show the close button or not
                        //    hideAfter: 5000,              // `false` to make it sticky or time in miliseconds to hide after
                        //    stack: 5,                     // `fakse` to show one stack at a time count showing the number of toasts that can be shown at once
                        //    icon: 'sucsses',
                        //});
                    }
                }
                else {//if partial view set it to modal otherwise print message
                    $(form).trigger("reset");
                    $('#alert')
                        .html(`<div class="alert alert-dismissible alert-danger show fade" role="alert">
                                <strong>${data.message}</strong>
                                <span type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close">
                                </span>
                            </div>`);
                    
                    if (data.html !== undefined && data.html !== '' && data.html !== null) {
                        $(modal).find(".modal-body").html(data.html);
                    }
                    else if ($('#alert').hasClass("refresh-table")) {
                        $(modal).modal('hide');
                        $('.datatable').DataTable().ajax.reload();
                    }
                    else {
                        $(modal).modal('hide');
                    }
                    $('.error-label').show()
                    $('.error-label').text(data.message);
                    if ($(modal).hasClass("contain-photo")) {
                        $('input[type=file]').each(function () {
                            let file = $(this);
                            let closestClass = $(file).closest('.has-file');
                            //1-set upload file
                            let getUploadFile = $(closestClass).find('.upload');
                            let getFileData = $(closestClass).find('.fileData');
                            let getfileDataName = $(closestClass).find('.fileDataName');
                            let getfileDataContentType = $(closestClass).find('.fileDataContentType');
                            if ($($(getFileData)[0]).val() != '' && $($(getFileData)[0]).val() != undefined) {
                                let fileData = dataURLtoFile($($(getFileData)[0]).val(), $(getfileDataName).val(), $(getfileDataContentType).val());
                                let dataTransferData = new DataTransfer();
                                dataTransferData.items.add(fileData);
                                getUploadFile[0].files = dataTransferData.files;
                            }
                            //2-set image 
                            let elementImage = $(closestClass).find('.upload-file');
                            let isPDF = $(elementImage).hasClass('is-pdf');
                            if (isPDF) {
                                $(elementImage).attr('src', "/img/pdf-placeholder.png");
                            }
                            else {
                                let reader = new FileReader();
                                reader.onload = function (e) {
                                    $(elementImage).attr('src', e.target.result);
                                }
                                if (getUploadFile[0] != undefined && getUploadFile[0].files[0] != undefined)
                                    reader.readAsDataURL(getUploadFile[0].files[0]);
                            }
                        });
                    }
                    // If there is a map in a modal re initialize it
                    let modalHasMap = $(modal).find('.map').val();
                    if (modalHasMap != undefined) {
                        modalId = $(modal).attr('id');
                        initMap();
                    }

                    GeneratePlugins(modal);
                }
            },
            error: function (err) {
                $(element).prop('disabled', false);
                console.log(err);
            }
        });
    //}
});


function dataURLtoFile(dataurl, filename, fileContentType) {
    var
        bstr = atob(dataurl),
        n = bstr.length,
        u8arr = new Uint8Array(n);

    while (n--) {
        u8arr[n] = bstr.charCodeAt(n);
    }
    return new File([u8arr], filename, { type: fileContentType });
}


$(document).on('hidden.bs.modal', ".modal", function (e) {
    let form = $(this).find("form");
    $(form).trigger("reset");
    $(this).find('.summernote').summernote('reset');
    //reset unobtrusive validation summary, if it exists
    $(form).find("[data-valmsg-summary=true]")
        .removeClass("validation-summary-errors")
        .addClass("validation-summary-valid")
        .find("ul").empty();

    //reset unobtrusive field level, if it exists
    $(form).find("[data-valmsg-replace]")
        .removeClass("field-validation-error")
        .addClass("field-validation-valid")
        .empty();

    //reset repeater
    let repeaterValue = $("#repeater-count").val();
    if (repeaterValue != undefined && repeaterValue != null && repeaterValue != '') {
        window.id = 0;
        $(form).find('[data-repeater-item]').remove();
    }
    

    $(form).find(".select2").val("").trigger('change');
    $('.error-label').hide()
});

$(document).on('shown.bs.modal', ".modal", function (e) {
    let modal = $(this);
    GeneratePlugins(modal);
    $.validator.unobtrusive.parse("form");

    let hasClass = $(modal).hasClass('get-files');
    if (hasClass)
        loadFiles();

    let modalHasMap = $(this).find('.map').val();
    if (modalHasMap != undefined) {
        modalId = $(this).attr('id');
        initMap();
    }

    //TimeZone
    AdjustModalInputsTimezone(modal);

    $('.error-label').text('')
});


function AdjustModalInputsTimezone(modal) {
   // Before using it make sure that those input default value (sent from server) are in proper utc time
    var localTimeInputs = $(modal).find('.client-dateTime')
    if (localTimeInputs != undefined) {

        let dateTimeInputSupportedFormat = "yyyy-MM-DDTHH:mm";
        let timeInputSupportedFormat = "HH:mm";
        let dateInputSupportedFormat = "yyyy-MM-DD";
        localTimeInputs.each(function () {

            let clientInput = $(this);
            let localDateTime;
            //for date and time elements
            let fullDateValueHolderElement

            // Put different input types in here
            switch (clientInput.attr("type")) {

                case "datetime-local":

                    localDateTime = moment.utc(clientInput.val()).local();

                    $(clientInput).attr('value', localDateTime.format(dateTimeInputSupportedFormat));

                    break;
                
                case "time":

                    fullDateValueHolderElement = $(clientInput).nextAll('.full-date-value-holder').first();
                    localDateTime = moment.utc(fullDateValueHolderElement.val()).local();

                    $(clientInput).attr('value', localDateTime.format(timeInputSupportedFormat));

                    break;

                case "date":

                    fullDateValueHolderElement = $(clientInput).nextAll('.full-date-value-holder').first();
                    localDateTime = moment.utc(fullDateValueHolderElement.val()).local();

                    $(clientInput).attr('value', localDateTime.format(dateInputSupportedFormat));

                    break;
            }

        });

    }
}

function GeneratePlugins(modal)
{
    //repeater
    let deleteURL = $(modal).attr('repeater-delete-url');
    let deleteMessage = $(modal).find(".repeater-delete-message");
    let isRepeaterHasData = $("#repeater-has-data").val() == 'true' ? true : false;
    window.id = $("#repeater-count").val();
    $(modal).find('.repeater').repeater({
        defaultValues: {
            'id': window.id,
        },
        initEmpty: !isRepeaterHasData,
        show: function () {
            $(this).slideDown();
            let counter = 0;

            $(this).find('.select2-container').remove();
            $(modal).find('.select2.repeater-select2').each(function (e) {
                $(this).attr("id", counter + "-repeater-select2");
                counter = counter + 1;
            });
            $('.select2.repeater-select2').select2({
                width: '100%',
                //dropdownParent: $(this)
            });
            $(this).find('.select2-container').css('width', '100%');

            AdjustSummerNotesInitialization(modal);

            AdjustMultipleSelect2(modal);
        },
        hide: function (deleteElement) {
            Swal.fire({
                title: deleteMessage.val(),
                showCancelButton: true,
                confirmButtonText: 'Delete',
            }).then((result) => {
                if (result.isConfirmed) {
                    
                    //console.log($('.repeater').repeaterVal());
                    $.ajax({
                        type: "POST",
                        url: deleteURL,
                        data: { id: $(this).find(".id").val() },
                        success: function (data) {
                            if (data.isSuccess) {
                                window.id--;

                                $(this).slideUp(deleteElement);

                                if (data.value != null)
                                {
                                    if (data.value.handleActivateDeactivateAddButtonCase) {

                                        let repeaterCount = $(modal).find('.repeate-data').length - 1;

                                        if (repeaterCount < data.value.repeaterLimit)
                                            $(modal).find('#repeater-add').removeAttr('disabled');
                                    }
                                }
                               
                            }
                            if (data.message != null) {
                                $('#alert')
                                    .html(`<div class="alert alert-dismissible ${data.isSuccess ? 'alert-success' :'alert-danger'} show fade" role="alert">
                                <strong>${data.message}</strong>
                                <span type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close">
                                </span>
                            </div>`);
                                if (data.isSuccess) {
                                    $('.error-label').removeClass('text-danger');
                                    $('.error-label').addClass('success-text');
                                }
                                else {
                                    $('.error-label').addClass('text-danger');
                                    $('.error-label').removeClass('success-text');
                                }   
                                $('.error-label').show()
                                $('.error-label').text(data.message); 
                            }
                        },
                        error: function (err) {
                            console.log(err);
                        }
                    });
                }
            });
        },
        ready: function (setIndexes) {
           // console.log(setIndexes);
        }
    });

    AdjustSummerNotesInitialization(modal);
    //$(modal).find('.summernote').summernote({
    //    dialogsInBody: true,
    //    toolbar: [
    //        // [groupName, [list of button]]
    //        ['style', ['style']],
    //        ['font', ['bold', 'underline', 'clear']],
    //        ['color', ['color']],
    //        ['para', ['ul', 'ol', 'paragraph']],
    //        ['table', ['table']],
    //        ['insert', ['link', 'picture', 'video']],
    //        ['view', ['fullscreen', 'help']]
    //    ]
    //});

    //$(modal).find('.select2').select2({
    //    width: '100%',
    //    dropdownParent: $(modal)
    //});

    AdjustMultipleSelect2(modal);

    //$(modal).find('.ajax-select2').select2({
    //    width: '100%',
    //    allowClear: true,
    //    minimumInputLength: 0,
    //    templateResult: formatState,
    //    templateSelection: formatState,
    //    dropdownParent: $(modal),
    //    ajax: {
    //        delay: 200,
    //        url: function () { return $(this).attr('select2-url') },
    //        data: function (params) {
    //            let query = {};
    //            let controllerName = $(this).attr('from-controller');
    //            let actionName = $(this).attr('from-action');
    //            if ($(this).attr('select2-obj') != undefined) {
    //                query = JSON.parse($(this).attr('select2-obj'));
    //                query.searchName = params.term || 'a';
    //                query.hasAllSelection = $(this).attr('has-all-selection') != undefined && $(this).attr('has-all-selection') =="true"?true:false;
    //                query.controllerName = controllerName != undefined ? controllerName : null;
    //                query.actionName = actionName != undefined ? actionName : null;
    //                return query;
    //            }
    //            else {
    //                query.searchName = params.term || 'a' ;
    //                query.hasAllSelection = $(this).attr('has-all-selection') != undefined && $(this).attr('has-all-selection') == "true" ? true : false;
    //                query.controllerName = controllerName != undefined ? controllerName : null;
    //                query.actionName = actionName != undefined ? actionName : null;
    //                return query;
    //            }

    //        },
    //        processResults: function (data, params) {
    //            // transforms the top-level key of the response object from 'items' to 'results'
    //            params.page = params.page || 1;
    //            return {
    //                results: data.items
    //            };
    //        }
    //    }
    //});


    $(modal).find('.datetimepicker').datetimepicker();
    //For paySlip picker
    $(modal).find('.yearmonthpicker').datetimepicker({ format: "YYYY MMMM" , defaultDate: new Date()});
}

function AdjustSummerNotesInitialization(modal) {
    $(modal).find('.summernote').each(function (index, element) {
        $(element).summernote({
            height: 150,
            dialogsInBody: true,
            height: 100,                 // set editor height
            minHeight: 50,             // set minimum height of editor
            maxHeight: 200,             // set maximum height of editor
            focus: false ,                // set focus to editable area after initializing summernote
            toolbar: [
                // [groupName, [list of button]]
                ['style', ['style']],
                ['font', ['fontname', 'fontsize', 'bold', 'italic', 'underline', 'clear',]],
                ['color', ['forecolor']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['table', ['table']],
              //  ['height', ['height']]
               // ['insert', ['link', 'picture', 'video']],
               // ['view', ['fullscreen', 'help']],

            ]
        });
    });
}

function AdjustMultipleSelect2(modal) {

    $(modal).find('.select2').select2({
            width: '100%',
            dropdownParent: $(modal)
    });

    $(modal).find('.multiple-select2-with-no-clear-for-view').select2({
        width: '100%',
        allowClear: false,
        minimumInputLength: 1,
        templateResult: formatState,
        templateSelection: formatState,
        dropdownParent: $(modal)
    });

    $(modal).find('.ajax-select2').select2({
        width: '100%',
        allowClear: true,
        minimumInputLength: 0,
        templateResult: formatState,
        templateSelection: formatState,
        dropdownParent: $(modal),
        ajax: {
            delay: 200,
            url: function () { return $(this).attr('select2-url') },
            data: function (params) {
                let query = {};
                let controllerName = $(this).attr('from-controller');
                let actionName = $(this).attr('from-action');
                if ($(this).attr('select2-obj') != undefined) {
                    query = JSON.parse($(this).attr('select2-obj'));
                    query.searchName = params.term || '';
                    query.hasAllSelection = $(this).attr('has-all-selection') != undefined && $(this).attr('has-all-selection') =="true"?true:false;
                    query.controllerName = controllerName != undefined ? controllerName : null;
                    query.actionName = actionName != undefined ? actionName : null;
                    return query;
                }
                else {
                    query.searchName = params.term || '' ;
                    query.hasAllSelection = $(this).attr('has-all-selection') != undefined && $(this).attr('has-all-selection') == "true" ? true : false;
                    query.controllerName = controllerName != undefined ? controllerName : null;
                    query.actionName = actionName != undefined ? actionName : null;
                    return query;
                }

            },
            processResults: function (data, params) {
                // transforms the top-level key of the response object from 'items' to 'results'
                params.page = params.page || 1;
                return {
                    results: data.items
                };
            }
        }
    });
}

function formatState(opt) {
    let imageDefault = opt.element != undefined ? opt.element.getAttribute('has-default-image') != null ? opt.element.getAttribute('has-default-image') : "/img/user.jpg" : "/img/user.jpg";
    let img = opt.photoUrl == null ? imageDefault : localStorage.getItem("URLPhoto") + opt.photoUrl

    if (handlePreSelectionsForSelect2Modals(opt) != null)
        img = handlePreSelectionsForSelect2Modals(opt);

    var $opt = $(
        `<span><img src="${img}" class="circle-image"/> <span class="m-1">${opt.text}</span></span>`
    );
    return $opt;
};

function handlePreSelectionsForSelect2Modals(opt) {
    if (opt.element != undefined) {
        if (opt.element.getAttribute('pre-selected-image') != null) {
            return localStorage.getItem("URLPhoto") + opt.element.getAttribute('pre-selected-image');
        }
    }
}

$(document).on('click', '.select2-selection__clear', function () {
    let element = $(this);
    let mainDiv = $(element).closest('.form-group');
    let selection = $(mainDiv).find(".ajax-select2");
    $(selection).val(null).trigger('change');;
});


$(document).on('click', '.form-modal', function () {
     
    let modal = '#' + $(this).attr('modal-id');
    $(modal).attr("success-function-name", $(this).attr('success-function-name'));
    $(modal).attr("success-function-param", $(this).attr('success-function-param'));
    $($(modal).find('.auto-filled-field')).val($(this).attr('action-url').split('=')[1]);
    $($(modal).find('span[name="owner"]')).text($(this).attr('modal-owner'));
    $(modal).attr('action-url', $(this).attr('action-url').split('?')[0]);
    $(modal).modal('show');
});
$(document).on('click', ".ajax-modal", function (e) {
    let modal = '#' + $(this).attr('modal-id');
    $(modal).attr("success-function-name", $(this).attr('success-function-name'));
    $(modal).attr("success-function-param", $(this).attr('success-function-param'));
    $($(modal).find(".modal-body")).load($(this).attr('action-url'), showModal);
    function showModal() {
        $(modal).modal('show');
    }
});

$(document).on('click', ".form-with-id-modal", function (e) {
    let modal = '#' + $(this).attr('modal-id');
    $($(modal).find(".modal-body")).load($(this).attr('action-url'), showModal);
    $($(modal).find('span[name="owner"]')).text('(' + $(this).attr('modal-owner') + ')');
    $($(modal).find('.auto-filled-field')).val($(this).attr('action-url').split('=')[1]);
    function showModal() {
        $(modal).modal('show');
    }
});

$(document).on('click', ".load-partial-view-tab", function (e) {
    let tab = $($(this).attr('href'));
    let url = $(this).attr('action-url');
    $.ajax({
        type: "GET",
        url: url,
        success: function (data) {
            $(tab).html(data);
            $(tab).find('.select2').select2({
                width: '100%',
                //dropdownParent: $(this).closest(".tab-pane")
            });
            $.validator.unobtrusive.parse("form");
        },
        error: function (err) {
            console.log(err);
        }
    });
});

function loadFiles(){
    $('input[type=file]').each(function () {
        let file = $(this);
        let closestClass = $(file).closest('.has-file');
        let getUploadFile = $(closestClass).find('.upload');

        let getFileDataHidden = $(closestClass).find('.fileData');
        let getfileDataNameHidden = $(closestClass).find('.fileDataName');
        let getfileDataContentTypeHidden = $(closestClass).find('.fileDataContentType');
        if ($($(getFileDataHidden)[0]).val() != '' && $($(getFileDataHidden)[0]).val() != undefined) {
            let fileData = dataURLtoFile($($(getFileDataHidden)[0]).val(), $(getfileDataNameHidden).val(), $(getfileDataContentTypeHidden).val());
            let dataTransferData = new DataTransfer();
            dataTransferData.items.add(fileData);
            getUploadFile[0].files = dataTransferData.files;
        }
    });
}

$(document).on("change", ".upload", function () {
    var extension = $(this).val().split('.').pop().toLowerCase();
    let checkExtension = $(this).attr('accept').replaceAll('.', '').split(',');
    let errorMessage = $(this).attr("errorMessage");
    if ($.inArray(extension, checkExtension) == -1) {
        $.toast({
            heading: "Error",
            text: errorMessage,
            bgColor: '#f8d7da',
            textColor: '#842029',
            allowToastClose: true,
            hideAfter: 5000,
            stack: 5,
            icon: 'error',
        });
        return false;
    }
    else if (checkExtension.includes("pdf")){
        let imageFieldId = "#" + $(this).attr("image-field-Id");
        $(imageFieldId).attr('src', '/img/pdf-placeholder.png');
    }
    else if (this.files && this.files[0]) {
        let imageFieldId = "#" + $(this).attr("image-field-Id");
        let reader = new FileReader();
        reader.onload = function (e) {
            $(imageFieldId).attr('src', e.target.result);
        }
        reader.readAsDataURL(this.files[0]);
    }
});


$(document).on('click', ".submit-tab", function (e) {
    e.preventDefault();
    let element = $(this);
    let container = $($(element).attr("container-id"));
    let form = $(container).find("form");
    let validators = form.validate();
    let isValid = form.valid();
    if (isValid) {
        $(element).prop('disabled', true);
        $.ajax({
            type: "POST",
            url: $(element).attr('action-url'),
            processData: false,
            contentType: false,
            data: new FormData(form[0]),
            success: function (data) {
                $(element).prop('disabled', false);
                if (data.isSuccess) {
                    //TODO: reset form validators
                    if (data.html !== undefined && data.html !== '' && data.html !== null) {
                        $($(element).attr("container-id")).html(data.html);
                    }
                    $('#alert')
                        .html(`<div class="alert alert-dismissible alert-success show fade" role="alert">
                        <strong>${data.message}</strong>
                        <span type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close">
                        </span>
                    </div>`);
                    $('.error-label').show()
                    $('.error-label').text(data.message);
                    //$.toast({
                    //    heading: "Done",
                    //    text: data.message,
                    //    bgColor: '#d1e7dd',              // Background color for toast
                    //    textColor: '#0f5132',            // text color
                    //    allowToastClose: true,       // Show the close button or not
                    //    hideAfter: 5000,              // `false` to make it sticky or time in miliseconds to hide after
                    //    stack: 5,                     // `fakse` to show one stack at a time count showing the number of toasts that can be shown at once
                    //    icon: 'sucsses',
                    //});
                    if ($(element).attr("success-function-name") != undefined) {
                        let param = $(element).attr("success-function-param");
                        var sucessFunction = `${$(element).attr("success-function-name")}(${param == undefined ? '' : `'${param}'`})`;
                        var ret = eval(sucessFunction);
                    }
                }
                else {//if partial view set it to modal otherwise print message
                    if (data.html !== undefined && data.html !== '' && data.html !== null) {
                        $(container).html(data.html);
                    }
                    $('#alert')
                        .html(`<div class="alert alert-dismissible alert-danger show fade" role="alert">
                                <strong>${data.message}</strong>
                                <span type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close">
                                </span>
                            </div>`);
                    $('.error-label').text(data.message);
                    //$.toast({
                    //    heading: "Error",
                    //    text: data.message,
                    //    bgColor: '#f8d7da',
                    //    textColor: '#842029',
                    //    allowToastClose: true,
                    //    hideAfter: 5000,
                    //    stack: 5,
                    //    icon: 'error',
                    //});
                }
            },
            error: function (err) {
                $(element).prop('disabled', false);
                console.log(err);
            }
        });
    }
});

$(document).ajaxError(function (event, jqxhr, settings, exception) {
   // 
    if (jqxhr.status == 401) {
        // unauthorized
        window.location.href = '/Account/Login';
    }
});

//function loadDropdown(dropdownId, url, message = "please select") {
//    $.ajax({
//        url: url,
//        type: 'GET',
//        success: function (response) {
//            var $dropdown = $("#" + dropdownId);
//            $dropdown.empty();
//            $dropdown.append($("<option />").val(null).text(message));
//            $.each(response, function (i, item) {
//                $dropdown.append($("<option />").val(item.id).text(item.name));
//            });
//        },
//        error: function (x, e) { }
//    });
//}
function loadDropdown(dropdownId, url, lang, message) {
     
    $.ajax({
        url: url,
        type: 'GET',
        success: function (response) {
            var $dropdown = $("#" + dropdownId);
            $dropdown.empty();
            $dropdown.append($("<option />").val(null).text(message));
            $.each(response, function (i, item) {
                optionValue = item.id;
                if (lang == "ar-EG") {
                    optionText = item.ArName;
                }
                else {
                    optionText = item.EnName;
                }
                $dropdown.append(new Option(optionText, optionValue));
            });
        },
        error: function (x, e) { }
    });
}
function loadDropdownNoPleaseSelect(dropdownId, url) {
    $.ajax({
        url: url,
        type: 'GET',
        success: function (response) {
            var $dropdown = $("#" + dropdownId);
            $dropdown.empty();
            $.each(response, function (i, item) {
                $dropdown.append($("<option />").val(item.id).text(item.name));
            });
        },
        error: function (x, e) { }
    });
}

function updateCoordinates(url) {
    $.ajax({
        type: "GET",
        url: url,
        dataType: "json",
        success: function (data) { 
            localStorage.setItem('latitude', data.latitude);
            $("#latitude").val(data.latitude);
            $("#longitude").val(data.longitude);
            initMap();
        },
        error: function (x, e) {
        }
    });
}
function loadDropdownElement(element, url, message = "please select") {
    $.ajax({
        url: url,
        type: 'GET',
        success: function (response) {
            var $dropdown = $(element);
            $dropdown.empty();
            $dropdown.append($("<option />").val(null).text(message));
            $.each(response, function (i, item) {
                $dropdown.append($("<option />").val(item.id).text(item.name));
            });
        },
        error: function (x, e) { }
    });
}

function loadGrid(url, objFilter) {
    $.ajax({
        type: "POST",
        url: url,
        data: objFilter,
        success: function (data) {
            $('#grid').html(data);
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function Base64ToBytes(base64) {
    let s = window.atob(base64);
    let bytes = new Uint8Array(s.length);
    for (var i = 0; i < s.length; i++) {
        bytes[i] = s.charCodeAt(i);
    }
    return bytes;
};

function Export(url) {
    $.ajax({
        type: "GET",
        url: url,
        success: function (data) {
            if (data.isSuccess) {
                var bytes = Base64ToBytes(data.value.fileContents);
                //Convert Byte Array to BLOB.
                var blob = new Blob([bytes], { type: data.value.contentType });
                //var blob = new Blob([bytes], { type: "application/octetstream" });
                var link = document.createElement('a');
                link.href = window.URL.createObjectURL(blob);
                link.download = data.value.fileDownloadName;
                link.click();

                $.toast({
                    heading: "Done",
                    text: data.message,
                    bgColor: '#d1e7dd',              // Background color for toast
                    textColor: '#0f5132',            // text color
                    allowToastClose: true,       // Show the close button or not
                    hideAfter: 5000,              // `false` to make it sticky or time in miliseconds to hide after
                    stack: 5,                     // `fakse` to show one stack at a time count showing the number of toasts that can be shown at once
                    icon: 'sucsses',
                });
            }
            else {
                $.toast({
                    heading: "Error",
                    text: data.message,
                    bgColor: '#f8d7da',
                    textColor: '#842029',
                    allowToastClose: true,
                    hideAfter: 10000,
                    stack: 5,
                    icon: 'error',
                });
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function RemoveLabel(className) {
    $(`.${className}`).hide();
}

function deletePhoto(getElement) {
    let element = $(getElement);
    let modal = $(element).closest('.modal');
    let img = $(modal).find(".upload-file");
    $(img).attr("src", $(modal).find(".default-photo").val());

    let file = $(modal).find(".upload");
    $(file).val(null);

    let photoData = $(modal).find(".photo-url");
    $(photoData).val('');
}

function loadDefinitions(definitionType,selectionMessage,defaultLabelName) {
    let definitionTypeValue = $(definitionType).val();
    let labelName = $(definitionType).find('option:selected').text();
    switch (definitionTypeValue) {
        case "1": //Employees
            let $dropdown = $(".to-select");
            $dropdown.empty();
            $(".to-select").removeAttr('disabled');
            $(".to-label").text(labelName);
            $(".to-select").addClass("ajax-select2");

            $('.ajax-select2').select2({
                width: '100%',
                minimumInputLength: 0,
                templateResult: formatState,
                templateSelection: formatState,
                ajax: {
                    delay: 200,
                    url: function () { return $(this).attr('select2-url') },
                    data: function (params) {
                        let query = {};
                        let controllerName = $(this).attr('from-controller');
                        let actionName = $(this).attr('from-action');
                        if ($(this).attr('select2-obj') != undefined) {
                            query = JSON.parse($(this).attr('select2-obj'));
                            query.hasAllSelection = $(this).attr('has-all-selection') != undefined && $(this).attr('has-all-selection') == "true" ? true : false;
                            query.searchName = params.term || '';
                            query.controllerName = controllerName != undefined ? controllerName : null;
                            query.actionName = actionName != undefined ? actionName : null;
                            return query;
                        }
                        else {
                            query.searchName = params.term || '';
                            query.hasAllSelection = $(this).attr('has-all-selection') != undefined && $(this).attr('has-all-selection') == "true" ? true : false;
                            query.controllerName = controllerName != undefined ? controllerName : null;
                            query.actionName = actionName != undefined ? actionName : null;
                            return query;
                        }

                    },
                    processResults: function (data, params) {
                        // transforms the top-level key of the response object from 'items' to 'results'
                        params.page = params.page || 1;
                        return {
                            results: data.items
                        };
                    }
                }
            });
            break;

        case "2": //Department
            loadDefinitionsDropdown(labelName, '/Core/Notifications/GetLookup?typeLookup=' + definitionTypeValue, selectionMessage);
            break;

        case "3": //Category
            loadDefinitionsDropdown(labelName, '/Core/Notifications/GetLookup?typeLookup=' + definitionTypeValue, selectionMessage);
            break;

        case "4": //Branch
            loadDefinitionsDropdown(labelName, '/Core/Notifications/GetLookup?typeLookup=' + definitionTypeValue, selectionMessage);
            break;

        case "5": //EmployeeGroup
            loadDefinitionsDropdown(labelName, '/Core/Notifications/GetLookup?typeLookup=' + definitionTypeValue, selectionMessage);
            break;

        case "6": //FunctionDepartment
            loadDefinitionsDropdown(labelName, '/Core/Notifications/GetLookup?typeLookup=' + definitionTypeValue, selectionMessage);
            break;


        default:
            let element = $(definitionType);
            let dropdown = $(element).closest('form');
            let to = $(dropdown).find(".to-select");
            $(to).attr('disabled', 'true');
            $(to).empty();
            $(".to-label").text(defaultLabelName);
            break;
    }
}

function loadDefinitionsDropdown(labelName, url, message) {
    $(".to-select").removeClass("ajax-select2");
    $(".to-select").select2({
        width: '100%'
    });
    $.ajax({
        url: url,
        type: 'GET',
        success: function (response) {
            let $dropdown = $(".to-select");
            $dropdown.empty();
            $dropdown.append($("<option />").val(null).text(message));
            $.each(response, function (i, item) {
                $dropdown.append($("<option />").val(item.id).text(item.name));
            });
            $(".to-select").removeAttr('disabled');
            $(".to-label").text(labelName);
        },
        error: function (x, e) { }
    });
}


function getDifferenceTime(fromTime, toTime) {
    let diff = (new Date(moment().format('YYYY-MM-DD') + " " + toTime)).getTime() - (new Date(moment().format('YYYY-MM-DD') + " " + fromTime)).getTime();
    let seconds = Math.floor(Math.abs(diff) / 1000); //ignore any left over units smaller than a second
    let minutes = Math.floor(seconds / 60);
    seconds = (seconds % 60).toLocaleString('en-US', { minimumIntegerDigits: 2, useGrouping: false });
    let hours = (Math.floor(minutes / 60)).toLocaleString('en-US', { minimumIntegerDigits: 2, useGrouping: false });
    minutes = (minutes % 60).toLocaleString('en-US', { minimumIntegerDigits: 2, useGrouping: false });

    return `${hours}:${minutes}`;
}

let counter = 0;
function WeeksCounter() {
    $('#current').click(function () {
        counter = 0;
        updateSearchList();
    });
    $('#next').click(function () {
        counter--;
        updateSearchList();
    });
    $('#previous').click(function () {
        counter++;
        updateSearchList();
    });

    return counter

}
function updateSearchList() {
    let tableSearch = $('.datatable').DataTable();
    let searchlist = [];
    searchlist.push({ 'value': counter, 'key': 'WeekCounter' });
    searchlist = searchlist.concat(SearchList());

    if (searchlist.length > 0) {
        tableSearch.search(JSON.stringify(searchlist)).draw();
    }
}
function setClearInputFieldsOnUnload(inputFieldIds) {
    $(window).on('beforeunload unload', function () {
        inputFieldIds.forEach(function (id) {
            $('#' + id).val('');
        });
    });
}

function borderColorCharts() {
    let borderColor = [
        'rgba(50, 226, 108, 1.0)',
        'rgba(54, 200, 100, 1.0)',
        'rgba(59, 180, 120, 1.0)',
        'rgba(66, 170, 50, 1.0)',
        'rgba(70, 150, 180, 1.0)',
        'rgba(75, 160, 127, 1.0)',
        'rgba(83, 170, 108, 1.0)',
        'rgba(93, 180, 144, 1.0)',
        'rgba(103, 190, 152, 1.0)',
        'rgba(113, 200, 201, 1.0)',
        'rgba(123, 210, 180, 1.0)',
        'rgba(133, 220, 108, 1.0)'
    ];
    return borderColor;
}

function backgroundColorCharts() {
    let backgroundColor = [
        'rgba(50, 226, 108, 1.0)',
        'rgba(54, 200, 100, 1.0)',
        'rgba(59, 180, 120, 1.0)',
        'rgba(66, 170, 50, 1.0)',
        'rgba(70, 150, 180, 1.0)',
        'rgba(75, 160, 127, 1.0)',
        'rgba(83, 170, 108, 1.0)',
        'rgba(93, 180, 144, 1.0)',
        'rgba(103, 190, 152, 1.0)',
        'rgba(113, 200, 201, 1.0)',
        'rgba(123, 210, 180, 1.0)',
        'rgba(133, 220, 108, 1.0)'
    ];
    return backgroundColor;
}

function drawPieCharts(chartId, chartLabelName, labelArray, dataArray) {
    var borderColor = borderColorCharts();
    var backgroundColor = backgroundColorCharts();
    let pieChart = document.getElementById(chartId).getContext('2d');
    let drowPieChart = new Chart(pieChart, {
        type: 'pie',
        data: {
            labels: labelArray,
            datasets: [{
                barPercentage: 0.5,
                label: chartLabelName,
                data: dataArray,
                borderColor: borderColor,
                backgroundColor: backgroundColor,
                borderidth: 1
            }]
        },
    });
}
function drawBarCharts(chartId, chartLabelName, labelArray, dataArray) {
    var borderColor = borderColorCharts();
    var backgroundColor = backgroundColorCharts();
    let barChart = document.getElementById(chartId).getContext('2d');

    let maxValue = Math.max(...dataArray);
    let roundedMaxValue = Math.round(maxValue / 10);
    roundedMaxValue = roundedMaxValue == 0 ? 1 : roundedMaxValue;

    let drowBarChart = new Chart(barChart, {
        type: 'bar',
        data: {
            labels: labelArray,
            datasets: [{
                barPercentage: 0.5,
                label: chartLabelName,
                data: dataArray,
                borderColor: borderColor,
                backgroundColor: backgroundColor,
                borderWidth: 1
            }]
        },
        options: {
            legend: {
                display: true
            },
            plugins: {
                datalabels: {
                    display: true,
                    align: 'center',
                    anchor: 'center',
                    color: '#FFCE56',
                    formatter: function (value) {
                        return value;
                    }
                }
            },
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true,
                        stepSize: roundedMaxValue, // Set a specific step size
                        max: Math.ceil(Math.max(...dataArray) / roundedMaxValue) * roundedMaxValue // Round up to the nearest multiple of the step size
                    }
                }]
            }
        }
    });
}

function drawLineCharts(chartId, chartLabelName, labelArray, dataArray) {
    var borderColor = borderColorCharts();
    var backgroundColor = backgroundColorCharts();
    let barChart = document.getElementById(chartId).getContext('2d');

    let maxValue = Math.max(...dataArray);
    let roundedMaxValue = Math.round(maxValue / 10);
    roundedMaxValue = roundedMaxValue == 0 ? 1 : roundedMaxValue;

    let drowBarChart = new Chart(barChart, {
        type: 'line',
        data: {
            labels: labelArray,
            datasets: [{
                barPercentage: 0.5,
                label: chartLabelName,
                data: dataArray,
                borderColor: borderColor,
                backgroundColor: backgroundColor,
                borderWidth: 1
            }]
        },
        options: {
            legend: {
                display: true
            },
            plugins: {
                datalabels: {
                    display: true,
                    align: 'center',
                    anchor: 'center',
                    color: '#FFCE56',
                    formatter: function (value) {
                        return value;
                    }
                }
            },
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true,
                        stepSize: roundedMaxValue, // Set a specific step size
                        max: Math.ceil(Math.max(...dataArray) / roundedMaxValue) * roundedMaxValue // Round up to the nearest multiple of the step size
                    }
                }]
            }
        }
    });
}

