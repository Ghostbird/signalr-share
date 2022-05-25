# SignalR Share

This is a very simple .NET Core app with two signalR endpoints:

- `Join` Must be called with a string as group identifier. You'll subscribe to subsequent messages in the same group. 
- `Share` Must be called with a string as group identifier, and any object as state data. That state data will be passed to `Update()` called on all _other_ clients in the identified group.

## Build and run

1. Open folder in VSCode
2. Ensure VSCode's C# extension is installed
3. Press <kbd>F5</kbd> to build and run

## Running on a server

You can adapt this to your CORS needs, I instead run both my [SignalR client](https://github.com/ghostbird/dicefight) and this service on the same Apache virtual host. I proxy traffic on the `/share` endpoint to this service. These are the relevant lines of my configuration file.

```conf
RewriteEngine On
<Location /share>
  RewriteRule .* "ws://localhost:5000/share" [P,L]
</Location>
# If an existing asset or directory is requested go to it as it is
RewriteCond %{DOCUMENT_ROOT}%{REQUEST_URI} -f [OR]
RewriteCond %{DOCUMENT_ROOT}%{REQUEST_URI} -d
RewriteRule ^ - [L]

# If the requested resource doesn't exist, use index.html (SignalR client web-app)
RewriteRule ^ /index.html
```

This probably requires:
```bash
sudo a2enmod proxy_wstunnel rewrite
```
