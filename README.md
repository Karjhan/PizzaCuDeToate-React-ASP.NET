# Pizza-Cu-De-Toate

## Description
Some of us have been in a situation where we're really hungry and don't know what to eat.
If you are a pizza, or shawarma lover, this might be your solution. If not, then keep an eye out
because we are ever extending.

"Pizza-Cu-De-Toate" translates to "Pizza with everything" and is a fullstack web application 
using ASP .NET Core and React which hosts a food delivery/restaurant website, with pizza and kebab 
as the main dishes.

## Tools and technologies learned

- recap React JS, Node JS, ASP.NET, REST APIs
- PostgreSQL DB hosted by Supabase
- Entity Framework for Code -> DB migrations
- Identity Framework and Google Oauth for authentication
- logged user and Identity roles for authorization
- React Three JS for 3D model additions
- React JS Particles and Toastify 
- Stripe API for payments and invoices
- Image import from GDrive
- use of Trello for backlog and organisation

## Installation

Use Node Package Manager [NPM](https://www.npmjs.com/) to install the required packages on the frontend.

### For frontend
```bash
cd PizzaCuDeToateAPI\PizzaCuDeToateAPI-frontend

npm install
```

Use NuGet Package Manager [NuGet](https://www.nuget.org/) to install the required packages on the backend.

### For backend
using nuget.exe in the original file:
```bash
cd PizzaCuDeToateAPI

nuget restore PizzaCuDeToateAPI.sln
```
or if your IDE has something like NuGet Restore, you can use it, for example in Rider: 

![Register](./screenshots/SS-NuGet-Restore.png)

In adition to that, the following data is required for the application to function correctly and it can be required 
from any of the original developers. Send a request to a contact info of one of the developers, asking for the 
following fields:

<ul>
    <li>ConnectionStrings</li>
    <ul>
        <li>PizzaCuDeToate_Db</li>
    </ul>
    <li>Frontend_Url -> (check your generated url)</li>
    <li>EmailConfiguration</li>
    <ul>
        <li>From</li>
        <li>SmtpServer</li>
        <li>Port</li>
        <li>Username</li>
        <li>Password</li>
    </ul>
    <li>JWT</li>
    <ul>
        <li>ValidAudience</li>
        <li>ValidIssuer</li>
        <li>Secret</li>
        <li>Subject</li>
    </ul>
    <li>Google</li>
    <ul>
        <li>ClientId</li>
        <li>ClientSecret</li>
    </ul>
    <li>StripeSettings</li>
    <ul>
        <li>SecretKey</li>
        <li>WebhookSecret</li>
        <li>AccountID</li>
    </ul>
</ul>

### After obtaining the required information 

You can put them in the *appsettings.json*(backend) in the following format
```
  "ConnectionStrings": {
    "PizzaCuDeToate_Db": ""
  },
  "AllowedHosts": "*",
  "Frontend_Url": "",
  "EmailConfiguration": {
    "From": "",
    "SmtpServer": "",
    "Port": 465,
    "Username": "",
    "Password": ""
  },
  "JWT": {
    "ValidAudience": "",
    "ValidIssuer": "",
    "Secret": "",
    "Subject": ""
  },
  "Google": {
    "ClientId": "",
    "ClientSecret": ""
  },
  "StripeSettings": {
    "SecretKey": "",
    "WebhookSecret": "",
    "AccountID": ""
  }
```
Or initialize a secret.json local file (it will be checked at runtime) and put all the appsettings.json content in there. 
For more detailed information check: [Safe storage of app secrets in development in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows).


#### Frontend Note
Unfortunately, depending on the generated port for backend, you may need to replace the frontend->backend request strings 
in the frontend source code, as they are static. A refactoring of the frontend side is in progress! In the backend, please
check the *StartupHostedService* class and replace the url in the command as necesary.


## Starting Commands

To start the backend server, from the original/root directory, go to backend in a terminal and type "dotnet run"
```bash
cd PizzaCuDeToateAPI

dotnet run
```
It would be ideal to setup the run configuration before. Example: In Rider IDE, I chose IIS server to set up the backend local host:

![RunConfiguration](./screenshots/SS-Run-Configuration.png)

To start the frontend, from the original/root directory, go to frontend in a terminal and type "npm run dev"
```bash
cd PizzaCuDeToateAPI\PizzaCuDeToateAPI-frontend

npm run dev 
```

For personal type checking, you can modify the *tsconfig.node.json* file in the frontend.

## Visuals

### Web UI of the backend API with Swagger
![SS-BackendAPI-Swagger](./screenshots/SS-BackendAPI-Swagger.png)
### Landing page
![SS-LandingPage-1](./screenshots/SS-LandingPage-1.png)

---

![SS-LandingPage-2](./screenshots/SS-LandingPage-2.png)

---

![SS-LandingPage-3](./screenshots/SS-LandingPage-3.png)
### Register page
![SS-RegisterPage-1](./screenshots/SS-RegisterPage-1.png)

---

![SS-RegisterPage-2](./screenshots/SS-RegisterPage-2.png)

---

![SS-RegisterPage-3](./screenshots/SS-RegisterPage-3.png)

---

![SS-RegisterPage-4](./screenshots/SS-RegisterPage-4.png)
### Email Confirmation page
![SS-OrderConfirmPage](./screenshots/SS-OrderConfirmPage.png)
### Login page
![SS-LoginPage-1](./screenshots/SS-LoginPage-1.png)

---

![SS-LoginPage-2](./screenshots/SS-LoginPage-2.png)

---

![SS-LoginPage-3](./screenshots/SS-LoginPage-3.png)
### Menu page 
![SS-MenuPage-1](./screenshots/SS-MenuPage-1.png)

---

![SS-MenuPage-2](./screenshots/SS-MenuPage-2.png)

---

![SS-MenuPage-3](./screenshots/SS-MenuPage-3.png)
### Order page
![SS-OrderPage-1](./screenshots/SS-OrderPage-1.png)

---

![SS-OrderPage-2](./screenshots/SS-OrderPage-2.png)

---

![SS-OrderPage-3](./screenshots/SS-OrderPage-3.png)
### Stripe payment
![SS-StripePayPage-1](./screenshots/SS-StripePayPage-1.png)

---

![SS-StripePayPage-2](./screenshots/SS-StripePayPage-2.png)

---

![SS-StripePayPage-3](./screenshots/SS-StripePayPage-3.png)

---

![SS-StripePayPage-4](./screenshots/SS-StripePayPage-4.png)

## Past intents
The pages *About us* and *Customize* are unfinished. The first one was meant to be a static page about the
restaurant and the creators. The second one was meant to be an integration of the OpenAI's chatGPT for 
development purposes for customization of pizzas and shawarmas.

## License
This project is licensed under the MIT License. See the LICENSE file for details.

## Contact
Feel free to contact me at: karjhan1999@gmail.com