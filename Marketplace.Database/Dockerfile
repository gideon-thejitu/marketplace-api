FROM  mcr.microsoft.com/mssql/server:2022-latest

USER root

ENV SA_PASSWORD @SecurePassword#
ENV ACCEPT_EULA Y

EXPOSE 1433

# Create app directory
WORKDIR /usr/src/app

RUN mkdir -p /var/opt/sqlserver
RUN mkdir /var/opt/sqlserver/data
RUN mkdir /var/opt/sqlserver/log
RUN mkdir /var/opt/sqlserver/backup

#RUN chown -R mssql:mssql /var/opt/sqlserver
#RUN chown -R mssql:mssql /var/opt/mssql
#
#RUN chown -R mssql:mssql /var/opt/mssql

# Copy initialization scripts
COPY entrypoint.sh /usr/src/app/entrypoint.sh
COPY run-initialization.sh /usr/src/app/run-initialization.sh
COPY hangfire.sql /usr/src/app/hangfire.sql

RUN chmod +x /usr/src/app/entrypoint.sh
RUN chmod +x /usr/src/app/run-initialization.sh

USER mssql

CMD /bin/bash ./entrypoint.sh
