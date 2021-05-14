pipeline {
    
    agent none

    environment {
        COMPOSE_PROJECT_NAME = "${env.JOB_NAME}-${env.BUILD_ID}"
    }
    stages {
      
        stage('Build') {

            agent any

            steps {
                
                echo sh(script: 'env|sort', returnStdout: true)

                sh  '''
                    cd ./OCodigoWebApp/
                    docker build . -f ./OCodigoWebApp/Dockerfile -t registry.oragon.io/demo/ocodigo_webapp:${BRANCH_NAME:-master}
               
                '''

            }

        }

        stage('Publish') {

            agent any

            when { buildingTag() }


            steps {
                
                sh  '''

                    docker tag registry.oragon.io/demo/ocodigo_webapp:${BRANCH_NAME:-master} registry.oragon.io/demo/ocodigo_webapp:latest

                    docker push registry.oragon.io/demo/ocodigo_webapp:${BRANCH_NAME:-master} 
                    
                    docker push registry.oragon.io/demo/ocodigo_webapp:latest
               
                '''
                

                withCredentials(bindings: [sshUserPrivateKey(credentialsId: 'SERVER_ADMIN', \
                                             keyFileVariable: 'SERVER_ADMIN_KEY', \
                                             passphraseVariable: 'SERVER_ADMIN_PWD', \
                                             usernameVariable: 'SERVER_ADMIN_USER')]) {
                    sh  '''
                    
                     docker pull registry.oragon.io/demo/ocodigo_webapp:${BRANCH_NAME:-master}

                     docker service update --image registry.oragon.io/demo/ocodigo_webapp:${BRANCH_NAME:-master}  --env-add NODE_ID="{{.Node.ID}}" --env-add APP_VERSION=${BRANCH_NAME:-master} ocodigo_webapp

                    '''
                }                

            }

        }


 
    }
    post {

        always {
            node('master'){
                
                sh  '''
               
                '''
            }
        }
    }
}