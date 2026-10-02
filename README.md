PREGUNTA 7: BUENAS PRÁCTICAS Y REFLEXIÓN

a. ¿Qué dificultades tuviste durante el scaffolding y cómo las solucionaste?

Durante el scaffolding tuvimos que asegurarnos de que los paquetes de Entity Framework estuvieran instalados en los proyectos correspondientes y que la conexión apuntara a nuestra base de datos local, luego pudimos generar las entidades y el TallerDbContext.

La principal dificultad apareció al autogenerar el controlador de TipoServicio. Visual Studio mostraba el error “Invalid --model 'TipoServicio'”, aunque la entidad ya existía en la biblioteca de clases. Para resolverlo usamos dotnet-aspnet-codegenerator desde PowerShell, indicando el nombre completo de la entidad y del DbContext. También instalamos Microsoft.EntityFrameworkCore.Tools en la Web API, porque el generador lo solicitaba. Finalmente, se creó correctamente el controlador con las operaciones CRUD.

b. ¿Qué diferencias notaste entre usar un controlador autogenerado y uno con repository? ¿Qué enfoque prefieres?

El controlador autogenerado permite avanzar rápidamente porque crea los métodos básicos para consultar, registrar, actualizar y eliminar información. En nuestro controlador de TipoServicio, estas operaciones se realizan directamente mediante el DbContext.

Con el patrón repository, el acceso a los datos se separa del controlador mediante una interfaz y una clase que la implementa. Esto requiere más archivos y configuración, pero facilita organizar el código y realizar cambios sin concentrar toda la lógica en el controlador.

Preferimos usar repository cuando el sistema tiene más reglas de negocio o puede crecer. Para una entidad sencilla como TipoServicio, el controlador autogenerado resulta práctico.

c. ¿Qué buenas prácticas implementaste en código real y por qué?

Separamos la solución en una Web API y una biblioteca de clases para organizar las responsabilidades. Las entidades quedaron en Core y el DbContext en Infrastructure.

Guardamos la cadena de conexión en appsettings.json y registramos TallerDbContext mediante inyección de dependencias en Program.cs, de ese modo la conexión queda centralizada y el controlador recibe el contexto que necesita.

En la base de datos usamos claves primarias autogeneradas, claves foráneas, esto ayuda a mantener la consistencia de los datos.

El controlador generado utiliza operaciones asíncronas y devuelve respuestas HTTP según el resultado, como 201 al crear un registro, 204 al actualizarlo o eliminarlo y 404 cuando el registro consultado no existe.

d. ¿Usaste herramientas de IA en esta práctica? ¿Qué herramientas usaste y en qué parte del proyecto?

Sí, usamos Claude como apoyo para preparar el script de SQL Server y los comandos de scaffolding, y encontrar una alternativa cuando falló la generación del controlador, también lo usamos para revisar los cambios subidos al repositorio.
