# Host ASP.NET Core on Raspberry PI with Nginx

This document describes how to install and configure Nginx as reverse proxy on a Raspberry PI

## Step-By-Step
The following section describes step-by-step how to setup Nginx:

1. Install Nginx
```
sudo apt-get install nginx
```

2. Start Nginx Service
```
sudo service nginx start
```

3. Adapt Nginx Configuration
```
sudo nano /etc/nginx/sites-available/default
```
Adapt location settings like this:
```
    location / {
        proxy_pass         http://127.0.0.1:8080;
        proxy_http_version 1.1;
        proxy_set_header   Upgrade $http_upgrade;
        proxy_set_header   Connection keep-alive;
        proxy_set_header   Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto $scheme;
    }
```
4. Validate and Reload Configuration
```
sudo nginx -t
sudo nginx -s reload
```
## Open Point

gRpc connection is currently not configured to be proxied by nginx.

## Documentation on the Web
You can find more information here:

[Microsoft: Host ASP.NET Core on Linux with Nginx](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-5.0)
