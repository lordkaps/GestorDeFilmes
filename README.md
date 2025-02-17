# Gestor de Filmes

## Descrição
O **Gestor de Filmes** é um aplicativo mobile desenvolvido em **.NET MAUI** para gerenciamento de filmes, permitindo que os usuários explorem informações sobre filmes e realizem autenticação via **OAuth**. O app integra-se com a API **TMDb** para buscar dados de filmes, armazena informação offline e suporta **notificações push** usando **Firebase Cloud Messaging**.

## Funcionalidades
- **Autenticação via OAuth** com a API TMDb
- **Busca de filmes** em tempo real
- **Armazenamento offline** de filmes favoritos usando Preferences
- **Diferenciação da interface** entre Android e iOS
- **Notificações push** com Firebase Cloud Messaging

## Tecnologias Utilizadas
- **.NET MAUI**
- **C#**
- **Preferences** (para armazenamento local)
- **API TMDb** (The Movie Database)
- **Firebase Cloud Messaging** (para notificações push)

## Configuração do Ambiente
### Requisitos
- .NET 7 ou superior
- SDK do .NET MAUI instalado
- Conta no TMDb para obter a chave da API
- Conta no Firebase para configurar as notificações push

### Passo a Passo
1. **Clone o repositório**
   ```bash
   git clone https://github.com/lordkaps/GestorDeFilmes.git
   cd GestorDeFilmes
   ```
2. **Configure as credenciais da API TMDb**
   - Crie um arquivo **secrets.json** no diretório do projeto e adicione:
     ```json
     {
       "TMDbApiKey": "SUA_CHAVE_AQUI"
     }
     ```
3. **Configure o Firebase**
   - Baixe o arquivo **google-services.json** (Android) e **GoogleService-Info.plist** (iOS) e coloque nas respectivas pastas do projeto.
4. **Restaure pacotes e rode o projeto**
   ```bash
   dotnet restore
   dotnet build
   dotnet maui run
   ```

## Estrutura do Projeto
```
GestorDeFilmes/
│── Models/          # Modelos de dados
│── Views/           # Telas do aplicativo
│── ViewModels/      # Lógica de negócio
│── Services/        # Integração com API TMDb e Firebase
│── Resources/       # Imagens, fontes e estilos
│── App.xaml         # Configuração global do aplicativo
│── MainPage.xaml    # Tela principal
└── Program.cs       # Ponto de entrada do app
```

