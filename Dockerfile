FROM mcr.microsoft.com/dotnet/core/sdk:7.0

RUN wget https://aka.ms/getvsdbgsh && \
    sh getvsdbgsh -v latest  -l /vsdbg
