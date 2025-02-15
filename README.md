# Gestor de Filmes

Gestor de Filmes é um aplicativo móvel desenvolvido com .NET MAUI, projetado para gerenciar e organizar sua coleção de filmes. Ele permite aos usuários buscar, favoritar e visualizar filmes, para Android e iOS.

Este projeto integra-se com a API do TMDb para buscar informações sobre filmes, suporta autenticação via OAuth, armazenamento offline e ~notificações push com Firebase Cloud Messaging~.

## Funcionalidades

- **Integração com TMDb API**: Pesquisa de filmes, obtenção de detalhes e imagens dos filmes diretamente da base de dados do TMDb.
- **Autenticação via OAuth**: Sistema de login para usuários com autenticação OAuth para garantir segurança.
- **Armazenamento Offline**: Armazenamento local para que os dados do usuário, como filmes favoritos, sejam acessíveis offline.
- ~**Notificações Push**: Envio de notificações push para alertar sobre novidades, lançamentos e outras atualizações.~
- **Interface Diferenciada**: Design otimizado para Android e iOS com um layout intuitivo.

## Tecnologias Usadas

- **.NET MAUI**: Framework cross-platform para construção de aplicativos móveis.
- **TMDb API**: API pública para obter dados sobre filmes e séries.
- **OAuth**: Autenticação segura utilizando OAuth 2.0.
- ~**Firebase Cloud Messaging**: Serviço de envio de notificações push para dispositivos móveis.~
  
## Pré-requisitos

- **.NET 7.0 ou superior**
- **Visual Studio 2022 ou superior** com suporte para .NET MAUI
- **Conta no Firebase** para configurar o envio de notificações push
