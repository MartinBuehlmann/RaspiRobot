# Setup Angular

This document guides you on how to setup angular for development.

## Angular on Ubuntu

As a first step we need to install nodejs:

> sudo apt-get install nodejs -y

You can verify the installation by checking the version of the installed nodejs:

> node --version

Next we need to install npm:

> sudo apt-get install npm -y

Again we can verify the installation of npm by checking its version:

> npm --version

Now we can install Angular CLI:

> sudo npm install -g @angular/cli

And again we can check the installed version:

> ng version

## Angular on Windows

As a first step we need to install nodejs. So go to https://nodejs.org and download the latest LTS Windows installer version.

Install nodejs and keep all the default settings.

You can verify the installation by checking the version of the installed nodejs:

> node --version

This will also install npm.

Again we can verify the installation of npm by checking its version:

> npm --version

Then install TypeScript:

> npm install -g typescript

Now we can install Angular CLI:

> sudo npm install -g @angular/cli

And again we can check the installed version:

> ng version

## How to Run an Existing Angular Application

Go to the directory of the app:

> cd Source/Frontend/raspi-robot-app

Install all dependencies:

> npm install

Start the application:

> ng serve -o
