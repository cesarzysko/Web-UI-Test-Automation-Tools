pipeline {
    agent any

    triggers {
        cron('15 22 * * *')
    }

    environment {
        DOTNET_CLI_TELEMETRY_OUTPUT = '1'
        DOTNET_NOLOGO = 'true'
    }

    stages {

        stage('Restore') {
            steps {
                bat 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                bat 'dotnet build --configuration Release --no-restore'
            }
        }

        stage('Selenium Tests') {
            steps {
                catchError(buildResult: 'FAILURE', stageResult: 'FAILURE') {
                    bat '''
                        dotnet test --configuration Release --no-build ^
                            --filter "Category=Selenium" ^
                            --logger "trx;LogFileName=selenium-tests.trx" ^
                            --results-directory TestResults/Selenium
                    '''
                }
            }
        }

        stage('API Tests') {
            steps {
                bat '''
                    dotnet test --configuration Release --no-build ^
                        --filter "Category=API" ^
                        --logger "trx;LogFileName=api-tests.trx" ^
                        --results-directory TestResults/API
                '''
            }
        }

    }

    post {
        always {
            mstest testResultsFile: 'TestResults/**/*.trx'
        }
    }
}