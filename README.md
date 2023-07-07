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

In adition to that, the following data is required for the application to function correctly and it can be required from any of the original developers. Send a request to a contact info of one of the developers, asking for the following fields:

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