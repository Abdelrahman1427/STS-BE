pipeline {
    agent any

    environment {
        GIT_REPO = 'https://github.com/Abdelrahman1427/STS-BE.git'  
        BRANCH = 'main'
        REMOTE_DIR = '/' // Replace with the actual remote directory
    }

    stages {
        stage('Pull App from Git') {
            steps {
                script {
                    git branch: BRANCH, url: GIT_REPO
                }
            }
        }

        stage('Install Dependencies and Build') {
            steps {
                script {
                 //   sh 'dotnet restore'

                    sh 'dotnet build --configuration Release'

                    // Publish the .NET app (this creates the output folder)
                    sh 'dotnet publish --configuration Release --output ./publish'
                }
            }
        }

        stage('Publish .NET App') {
            steps {
                script {
                    sh 'ls -l ./publish' // Output the publish folder contents
                }
            }
        }

        stage('Push Files to Local Machine') {
            steps {
                script {
                    // Ensure the remote directory exists and copy the published files
                    withCredentials([usernamePassword(credentialsId: 'Remote_machine', usernameVariable: 'REMOTE_USER', passwordVariable: 'REMOTE_PASS'),
                                     string(credentialsId: 'Remote_Host_IP', variable: 'REMOTE_HOST')]) {
                        sh """
                            sshpass -p '${REMOTE_PASS}' ssh ${REMOTE_USER}@${REMOTE_HOST} 'mkdir -p ${REMOTE_DIR}'
                            sshpass -p '${REMOTE_PASS}' scp -r ./publish/* ${REMOTE_USER}@${REMOTE_HOST}:${REMOTE_DIR}
                        """
                    }
                }
            }
        }
    }

    post {
        success {
            echo 'Pipeline completed successfully.'
        }
        failure {
            echo 'Pipeline failed.'
        }
    }
}
