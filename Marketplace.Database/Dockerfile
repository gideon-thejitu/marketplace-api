FROM  mcr.microsoft.com/mssql/server:2022-latest

USER root

ENV SA_PASSWORD @SecurePassword!
ENV ACCEPT_EULA Y

EXPOSE 1433

# Create app directory
WORKDIR /usr/src/app

# Copy initialization scripts
COPY entrypoint.sh /usr/src/app/entrypoint.sh
COPY run-initialization.sh /usr/src/app/run-initialization.sh
COPY hangfire.sql /usr/src/app/hangfire.sql

RUN ls

USER mssql

CMD /bin/bash ./entrypoint.sh
