# DevFreela

![devfreela-crop-video-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/6c04b248-e403-4217-b6af-32a3192d64d8)

### English

Complete REST API for a freelance projects management system.

## Technologies and Practices Used:
- ASP.NET Core with .NET 6
- Clean Architecture
- Entity Framework Core
- CQRS
- Repository Pattern
- API Validation
- Authentication and Authorization with JWT
- Unit Testing
- Messaging with RabbitMQ

## Features:
- Create, List, View Details, Update, and Delete Projects
- Start and Complete Projects
- Add Comments to Projects
- User Registration, Details, and Login

## Running the application
To run the project, you must first start RabbitMQ with the following command:

`docker run --rm -it -p 15672:15672 -p 5672:5672 rabbitmq:3-management`

Access: http://localhost:15672/

Log in with username/password: **guest**

## Frontend:
- was made with Vue.js 2
- using vue-router, vuex (for state management), axios, unity test, test e2e, vuetity, bootstrap-vue, vue-toasted
  
## Project setup
```
npm install
```

### Compiles and hot-reloads for development
```
npm run serve
```

### Compiles and minifies for production
```
npm run build
```

********************************************************
********************************************************

### Português

API REST completa de um sistema de projetos de freelances.

## Tecnologias e práticas utilizadas
- ASP.NET Core com .NET 6
- Arquitetura Limpa
- Entity Framework Core
- CQRS
- Padrão Repository
- Validação de APIs
- Autenticação e Autorização com JWT
- Testes Unitários
- Mensageria com RabbitMQ

## Funcionalidades
- Cadastro, Listagem, Detalhes, Atualização e Remoção de Projetos
- Início e Conclusão de Projetos
- Cadastro de Comentário em Projeto
- Cadastro, Detalhes e Login de Usuário

## Executando a aplicação
Para executar o projeto deve primeiro executar o rabbitmq com o comando:

`docker run --rm -it -p 15672:15672 -p 5672:5672 rabbitmq:3-management`

acessar: http://localhost:15672/

entrar com usuario/senha: **guest**

## Frontend:
- foi feito com Vue.js 2
- using vue-router, vuex (para gerenciamento de estado), axios, unity test, test e2e, vuetity, bootstrap-vue, vue-toasted

### Project setup
```
npm install
```

### Compiles and hot-reloads for development
```
npm run serve
```

### Compiles and minifies for production
```
npm run build
``` 
