# Sympli Seo Checker Solution

 > <cite>Prepared by Tuong Nguyen</cite>

## 1. Setup Source code

### a. Setup Back end
Update the application Url to run in `launchSettings.json` file. The url is `localhost:5118` in the current default setup.

### b. Setup Front end
Update API host in `src\environment.ts` file. Current setup it is `http://localhost:5118`.

## 2. Run the back end
- Swagger page
![Swagger page](assets/4.api-swagger.png)

- Postman called with invalid parameters and validation errors occurred
![Validate error](assets/5.api-validation.png)

- Postman called with valid parameters and search ranking has been responsed
![Search ranking response](assets/6.api-response.png)

## 3. Run the front end
- Initial page
![Initial page](assets/1.fe-init.png)

- Enter invalid values, the validation errors occurred
![Validation page](assets/2.fe-validation.png)

- Submit the valid values, the search ranking will be responsed.
![Search ranking response](assets/3.fe-response.png)
