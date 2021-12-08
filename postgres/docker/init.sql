CREATE USER db_admin WITH PASSWORD 'root';
CREATE USER db_user WITH PASSWORD 'user';

CREATE SCHEMA app;

-- Make sure db_admin has all privileges of db_user.
GRANT db_user TO db_admin;
GRANT db_admin TO superuser;

-- Grant privileges to existing objects.
GRANT ALL PRIVILEGES ON DATABASE minglass TO db_admin;
GRANT ALL PRIVILEGES ON SCHEMA app TO db_admin;
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA app TO db_admin;

GRANT USAGE ON SCHEMA app TO db_user;
GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA app TO db_user;

-- Grant privileges of objects created in the future by db_admin to db_user.
ALTER DEFAULT PRIVILEGES FOR USER db_admin IN SCHEMA app GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO db_user;