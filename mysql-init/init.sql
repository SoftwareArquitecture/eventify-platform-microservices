-- Crear bases de datos para cada microservicio
CREATE DATABASE IF NOT EXISTS eventify_iam;
CREATE DATABASE IF NOT EXISTS eventify_planning;
CREATE DATABASE IF NOT EXISTS eventify_profiles;
CREATE DATABASE IF NOT EXISTS eventify_operation;

-- Dar permisos al usuario 'eventify' en todas ellas
GRANT ALL PRIVILEGES ON eventify_iam.* TO 'eventify'@'%';
GRANT ALL PRIVILEGES ON eventify_planning.* TO 'eventify'@'%';
GRANT ALL PRIVILEGES ON eventify_profiles.* TO 'eventify'@'%';
GRANT ALL PRIVILEGES ON eventify_operation.* TO 'eventify'@'%';
FLUSH PRIVILEGES;