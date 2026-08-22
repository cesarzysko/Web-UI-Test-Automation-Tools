pipeline {
    agent any

    options {
        skipDefaultCheckout(true)
    }

    parameters {
        choice(
            name: 'SELENIUM_BROWSER',
            choices: ['Chrome', 'Firefox'],
            description: 'Selenium WebDriver to use for Selenium tests.'
        )
    }

    triggers {
        cron('15 22 * * *')
    }

    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = 'true'
        DOTNET_NOLOGO = 'true'
    }

    stages {

        stage('Checkout') {
            steps {
                cleanWs()
                checkout scm
            }
        }

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
                    withEnv(["Browser=${params.SELENIUM_BROWSER}"]) {
                        bat '''
                            dotnet test --configuration Release --no-build ^
                                --filter "Category=Selenium" ^
                                --logger "trx;LogFileName=selenium-tests.trx" ^
                                --results-directory TestResults/Selenium
                        '''
                    }
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

            archiveArtifacts (
                artifacts: 'Tests/bin/Release/net10.0/Logs/**/*',
                allowEmptyArchive: true
            )
        }
    }
}