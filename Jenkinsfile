pipeline {
    agent any

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

        stage('Tests') {
            steps {
                bat 'dotnet test --configuration Release --no-build'
            }
        }

    }
}