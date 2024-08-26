# DevFreela

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
