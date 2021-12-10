FROM mcr.microsoft.com/dotnet/core/sdk:3.1

RUN wget https://aka.ms/getvsdbgsh && \
    sh getvsdbgsh -v latest  -l /vsdbg
