# API com PostgreSQL, Redis e Dapper
API de CRUD feita em ASP.NET CORE a partir do template com controllers.

## Sobre o Projeto
Este projeto é uma API de CRUD simples feita no ASP.NET CORE que utiliza um banco de dados PostgreSQL, juntamente com Redis para cache dos dados vindo do banco, Dapper como micro ORM para acesso ao banco de dados.

Também é utilizado o Docker para a conteinerização das imagens que o projeto utiliza, e a biblioteca bcrypt para a realização de hash de senha para armazenamento no banco.

## Como rodar o projeto 
### Clone este repositório
```
git clone https://github.com/leandrorossi/redis-postgres-api.git
```

### Executando via dotnet CLI
Você pode rodar o projeto via dotnet CLI com os seguintes comandos:
```
dotnet run
```

### Executando via Docker Compose
O projeto possui um arquivo docker compose com as imagens configuradas, para rodar pelo docker execute:
```
docker compoose up -d
```

## Tecnologias utilizadas
As seguintes ferramentas foram usadas na construção do projeto:
- ASP.NET CORE
- PostgreSQL
- Redis
- Dapper
- Docker
- Bcrypyt

## Autor
 <img style="border-radius: 50%;" src="https://avatars2.githubusercontent.com/u/65093597?s=60&v=4" width="100px;" alt=""/>
 <br />
 <span><b>Feito por Leandro Rossi</b></span>
 <br />
 <br />

 [![Linkedin Badge](https://img.shields.io/badge/-Leandro-blue?style=flat-square&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/leandro-rossi-4769ab1a6/)](https://www.linkedin.com/in/leandro-rossi-4769ab1a6/)
 [![Outloook](https://img.shields.io/badge/le_andro18@hotmail.com-0078D4?style=flat-square&logo=microsoft-outlook&logoColor=white&link=mailto:le_andro18@hotmail.com)](mailto:le_andro18@hotmail.com)
