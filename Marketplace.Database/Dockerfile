FROM mcr.microsoft.com/mssql/server:2017-latest

# Create a config directory
RUN mkdir -p /usr/config
WORKDIR /usr/config

# Bundle config source
COPY Marketplace.Database/entrypoint.sh /usr/config/entrypoint.sh
COPY Marketplace.Database/configure-db.sh /usr/config/configure-db.sh

RUN echo ls

# Grant permissions for to our scripts to be executable
RUN chmod +x /usr/config/entrypoint.sh
RUN chmod +x /usr/config/configure-db.sh

ENTRYPOINT ["/usr/config/entrypoint.sh"]
