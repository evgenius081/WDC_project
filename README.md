# WDC_project
This project was developed as a project from Introduction into Cybersecurity (Wprowadzenie do Cyberbezpeczeństwa (pl.) - that's why project is called WDC_project). 
Title of the project is "Authorization in web-applications". For implementation I used ASP.NET, because I like C# and becuase it has built-in features I needed.
## What is authorization and why do we need it?
Authorization is process of giving/checking rights of subject to have access to/have something. Speaking about web-applications, we use authorization when we want to 
check if user should have access to some data or actions. 

For example, we have e-shop where users can see products, by them and leave opinion. Managers can create, update and delete products' info and delete users' 
reviews. Admin is a god of this web-application and can do anything. 

Firstly, we need to authenticate user, because we need to know if the person, who gave this username is actually this user. In my application, I use only passwords to
do so. If everything is correct, application generates token for it to know, that user is already authenticated.

And now, when we know that this person is actually this person, we can find out, if he has right to do smth. For this purpose, application uses token, generated while 
authentication, and checks, if user's data meets claims, set on this element of application.

This how authorization works :)
## Details of implementation
Application is rather abstract, so there is only 3 types of users and thats it. Users have id, name, username, password (hashed), token, role and age. 

The only action, requiring authorization, is reading all users' info. In my application, I use 2 types of authorization: by roles and by policies.

There are 3 roles:
- User - can only see his info
- Manager - can see shortened info of users (name, username and roles)
- Admin - can see everything

I also created some policies, such as:
- Managers can only see information about users, who are 18+ years old.
- There may be multiple admins, but see users' info is only John Doe's privilege
## Start-up
* Using Docker:
  1. Open console at main directory.
  2. Type next lines:
  ```console
  docker build -f docker -t wdc-project .
  docker run -d -p 5000:80 -- wdc-project wdc-project-container
  ```
* Using console:
  1. Open console at directory WDC_project.Web
  2. Type next line:
  ```console
  dotnet run
  ```
* Using MS Visual Studio or Jetbrains Rider Projects
  1. Open WDC_project.sln file with one of these IDEs.
  2. Run solution using WDC_project.Web configuration (not IIS Express!!!)
  
## Testing
As interface was not implemented, I used Postman app to test requests. To run tests, open WDC_project.postman.json using Postman app. 

To send [Auth] requests, you firstly need to authenticate using Authenticate request. At the body of this request you must enter username and password from the list 
of users. If entered data is correct, you will be authenticated and JWT token will be generated. Using this token (copy from respond on Authenticate request, then at 
Authorization section of request you must choose "Bearer Token" option, and paste token here), you can send requests and see the respond from my application. 
Application returns error codes as follows:
- 200 - OK, when everything is correct
- 201 - Created, when application successfully creates a user by Add request
- 401 - Unauthorized, when you are trying to send request without token
- 403 - Forbidden, when user's data doesn't meet the claims
- 404 - Not found, when trying to send unknown request
- 500 - Internal Error, when something goes wrong on the application side

If you get 200 error code, then you also will see the respond with the data you wanted.

Good luck using it!

## Notes
1. If you want more info about my application or theory and speak polish, you can read file WDC_sprawozdanie.pdf at the root of repo. If it's not enough, please 
contact me.
