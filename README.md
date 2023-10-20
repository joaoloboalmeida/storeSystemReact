João Antônio Azevedo Lobo de Almeida

- Descrição do que foi feito:
Foi utilizada a versão .NET 7, SQL Server e React.
Foi implementado um sistema de visualização e cadastro de clientes, além da possibilidade de vincular produtos a determinado cliente. Conforme os requisitos do projeto, a API foi construída com a utilização de Swagger e não é permitida a criação de dois clientes com o mesmo CPF, onde o campo criado no banco de dados é único. 
Foi utilizada a abordagem CodeFirst, gerando o banco de dados a partir de migration, com a utilização de Fluent APIs na definição dos campos das tabelas, como primary keys, foreign keys, índices, tipos de dados, nulidade e tamanho máximo. 
Além disso, foi utilizado o princípio da Inversão de Controle (IoC) com injeção de dependência, deixando de ter a dependência internamente na classe e passando para uma classe externa (a implementação deve depender da abstração, não o contrário - SOLID), bem como práticas do Clean Code. 
No frontend com React foi implementada a validação de CPF, conforme requisitos do projeto, com a lib cpf-cnpj-validator.
Na tela de criação de um novo cliente optei por utilizar os componentes de modal da lib reactstrap. 
Na tela para vincular produtos para um cliente, utilizei o componente Select da lib react-select de forma a permitir a seleção múltipla de produtos. 

- O que faria se tivesse mais tempo:
Teria aplicado de forma mais concreta a Clean Architecture no projeto, bem como teria implementado testes unitários. 
Além disso, criei diversos métodos para um crud completo, porém não houve tempo hábil para a implementação das telas com React, então é algo que eu com certeza daria andamento. 
Teria também estilizado mais o sistema como um todo.
Além disso, uma melhoria a ser feita seria a implementação de uma tela de login com autenticação com JWT. 
Outra melhoria que pensei seria a criação de categorias de produtos, permitindo a vinculação de diversos deles nas categorias. 