FROM microsoft/dotnet

COPY ./publish /app/identityserver4-manager
WORKDIR /app/identityserver4-manager
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

EXPOSE 9090

RUN ["dotnet", "IdentityServer4-Manager.dll"]

#RUN ["dotnet", "build"]


#ENTRYPOINT ["dotnet", "run", "--server.urls", "http://0.0.0.0:5000"]
