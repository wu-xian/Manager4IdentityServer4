FROM microsoft/dotnet

COPY ./src/IdentityServer4-Manager /app/IdentityServer4-Manager
WORKDIR /app/IdentityServer4-Manager
#RUN ["dotnet", "restore"]
#CMD rm /app/project.lock.json
#CMD rm /app/project.json
#COPY . /app
#RUN ["dotnet", "restore"]
#RUN ["dotnet", "build"]
#RUN ["dotnet", "ef", "database", "update"]
#ENTRYPOINT ["dotnet", "run", "--server.urls", "http://0.0.0.0:5000"]
#EXPOSE 5000/tcp

RUN ["ls"]

RUN ["dotnet", "restore", "-s", "http://192.168.0.102:9081/api/v2/"]

RUN ["dotnet", "build"]

EXPOSE 5000

ENTRYPOINT ["dotnet", "run"]
