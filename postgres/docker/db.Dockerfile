FROM postgres

ENV POSTGRES_USER 'superuser'
ENV POSTGRES_PASSWORD 'superroot'
ENV POSTGRES_DB 'minglass'
ENV PGDATA /var/lib/postgresql/data

COPY init.sql /docker-entrypoint-initdb.d/

RUN echo "init script copied."