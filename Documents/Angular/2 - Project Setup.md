# Project Setup

# Create a new Angular project

Go to the folder where you want to create your Angular based frontend and create a new project:

> ng new raspi-robot-app --no-standalone --routing

Answer the questions and your project will be created.
(I configured SCCS no Server-Side Renderig and Static Site Generation)

Test your new project by going into the app directory and run it:

> cd raspi-robot-app
> ng serve

The app will be accessible in the browser: http://localhost:4200

# Generate a new header component

New component can be created by:

> ng generate component header

It will be created in the src/app/header folder.

Replace content of the app.component.html with

> <app-header></app-header>
> 
> content

Save the file and you should see your header works in the browser.