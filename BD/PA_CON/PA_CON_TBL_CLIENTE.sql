USE [SIMPDB]

/*
===================================================================================================================================================
<<<<<<<<CREACION>>>>>>>
===================================================================================================================================================
AUTOR:          <ESTEBAN.NAVARRO>
FECHA:          <2022/09/17>
DESCRIPCION:   <SELECCIONA CLIENTES>
-------------------------------------------

=========================================COMPLETAR CON NUEVOS PARAMETROS============================================================================

EXEC dbo.PA_CON_CLIENTES 	@P_USUARIO = '',            -- varchar(20)
                                    @P_OPCION = 0,              -- smallint
                                    @P_ID = 0,					-- int
                                    @P_ESTADO = '',             -- char(1)
                                    @P_ESQUEMA = ''             -- varchar(20)

===================================================================================================================================================
<<<<<<<<MODIFICACIONES>>>>>>>

UTILIZAR LOS SIGUIENTES COMENTARIOS PARA DETALLAR LOS CAMBIOS EN EL CODIGO
--INICIO,TICKET=00000,RELEASE=000,NOMBRE.APELLIDO, COMENTARIOS SOBRE LOS CAMBIOS
--FIN,TICKET=00000,RELEASE=000,NOMBRE.APELLIDO, COMENTARIOS SOBRE LOS CAMBIOS
===================================================================================================================================================
*/
GO
CREATE PROCEDURE [dbo].[PA_CON_CLIENTES]
(
     @P_USUARIO VARCHAR(50) = '',		 --USUARIO DE LA APLICACION QUE EJECUTA
     @P_OPCION SMALLINT = 0,			 --OPCION SIRVE PARA CONFIGURAR COMPORTAMIENTOS O FLUJOS EN EL PROCESO (0 muestra todos los registros 1 muestra los registros filtadros por usuario)
     @P_ID BIGINT = 0,								/*ID DEL CLIENTE */
	 @P_NOMBRE VARCHAR(50) ='',						/*NOMBRE DEL USUARIO*/
	 @P_PRIMER_APELLIDO VARCHAR(50) = '',			/*PRIMER APELLIDO*/
	 @P_SEGUNDO_APELLIDO VARCHAR(50) = '',			/*SEGUNDO APELLIDO*/
     @P_CORREO_ELECTRONICO VARCHAR(50) = '',			/*CORREO_ELECTRONICO*/
	 @P_TELEFONO VARCHAR(15) = '',			        /*TELEFONO*/
	 @P_ESQUEMA VARCHAR(20) = ''         --Parametro que identifica la compa�ia
)
AS
BEGIN
    SET XACT_ABORT, NOCOUNT ON; --ADDED TO PREVENT EXTRA RESULT SETS FROM INTERFERING WITH SELECT STATEMENTS.
    --SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED; --SERIALIZABLE; -- ADD NOLOCK ALL TABLES 
    BEGIN TRY

        --/*MODIFICAR EN CASO DE SELECT*/
        --BEGIN TRANSACTION;
        --/*MODIFICAR EN CASO DE SELECT*/


        --++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ 
        -----------------------------                                    
        --DECLARACION Y ASIGNACION DE VARIABLES                                  
        -----------------------------
        BEGIN --INICIO-DECLARACION VARIABLES Y CONSTANTES
            BEGIN --INICIO-CONSTANTES
                DECLARE @P_MODO_EJECUCION SMALLINT
                    = 0,                                           --MODO DE EJECUCION, SIRVE PARA MOSTRAR BANDERAS O DATOS EN CASO DE NECESITAR VALIDAR, EJECUCION_DEPURACION=7, EJECUCION_NORMAL=0 
                        @P_IDENTIFICADOR_EXTERNO VARCHAR(50) = ''; --IDENTIFICADOR ENVIADO DESDE EL APLICATIVO, ESTE SE ALMACENA EN LAS BITACORAS DE EJECUCION O EN LAS BITACORAS DE EXCEPCIONES
                DECLARE @V_STR_ESQUEMA VARCHAR(500) = '';
                DECLARE @V_MODO_EJECUCION_NORMAL SMALLINT = 0;
                DECLARE @V_MODO_EJECUCION_DEPURACION SMALLINT = 7;
                DECLARE @V_OPCION_NORMAL SMALLINT = 0;
                DECLARE @V_ID_ORIGEN_WINDOWS_FORM INT = 1;
                DECLARE @V_DESCRIPCION_ORIGEN_WINDOWS_FORM VARCHAR(300) = 'WINDOWS FORM';
                DECLARE @V_ID_ORIGEN_WEB_SERVICE INT = 2;
                DECLARE @V_DESCRIPCION_ORIGEN_WEB_SERVICE VARCHAR(300) = 'WEB SERVICE';
                DECLARE @V_ID_ORIGEN_WEB_SITE INT = 3;
                DECLARE @V_DESCRIPCION_ORIGEN_WEB_SITE VARCHAR(300) = 'WEB SITE';
                DECLARE @V_ID_ORIGEN_PROCEDIMIENTO INT = 4;
                DECLARE @V_DESCRIPCION_ORIGEN_PROCEDIMIENTO VARCHAR(300) = 'PROCEDIMIENTO ALMACENADO';
                DECLARE @V_ID_ORIGEN_REPORTING_SERVICE INT = 5;
                DECLARE @V_DESCRIPCION_ORIGEN_REPORTING_SERVICE VARCHAR(300) = 'REPORTING SERVICE';
                DECLARE @V_ID_ORIGEN_ETL INT = 6;
                DECLARE @V_DESCRIPCION_ETL VARCHAR(300) = 'ETL';
                DECLARE @V_ID_ORIGEN_JOB INT = 7;
                DECLARE @V_DESCRIPCION_JOB VARCHAR(300) = 'JOB';
                DECLARE @V_ID_TIPO_ERROR INT = 1;
                DECLARE @V_DESCRIPCION_TIPO_ERROR VARCHAR(300) = 'ERROR';
                DECLARE @V_ID_TIPO_EXCEPCION INT = 2;
                DECLARE @V_DESCRIPCION_TIPO_EXCEPCION VARCHAR(300) = 'EXCEPCION-PERSONALIZADA';
                DECLARE @V_ID_TIPO_DATOS_XML SMALLINT = 1; --ID Tipo dato XML
            END; --FIN-CONSTANTES
            BEGIN --INICIO-SOBREESCRITURA DE VARIABLES
                DECLARE @V_IDENTIFICADOR_UNICO VARCHAR(50) = CONVERT(VARCHAR(50), NEWID());
            END; --FIN-SOBREESCRITURA DE VARIABLES
            BEGIN --INICIO-ADMINISTRACION ERRORES
                DECLARE @V_ERR_FK_UTL_PAR_PAIS SMALLINT = 0;
                DECLARE @V_ERR_USUARIO_CREACION VARCHAR(50) = ISNULL(@P_USUARIO, 'N/D');
                DECLARE @V_FEC_ERR_SISTEMA DATETIME = GETDATE();
                DECLARE @V_FEC_ERR_CREACION DATETIME = GETDATE();
                DECLARE @V_ERR_ID_SPID INT = @@SPID;
                DECLARE @V_ERR_PRIORIDAD INT = 0;
                DECLARE @V_ERR_SEVERIDAD INT = 0;
                DECLARE @V_ERR_ESTADO INT = 0;
                DECLARE @V_ERR_BASE_DATOS VARCHAR(300) = ISNULL(DB_NAME(), 'N/D');
                DECLARE @V_ERR_SERVIDOR VARCHAR(300) = ISNULL(@@SERVERNAME, 'N/D');
                DECLARE @V_ERR_PROCESO VARCHAR(300) = ISNULL(OBJECT_NAME(@@PROCID), 'N/D'); --OBTIENE EL NOMBRE DEL PROCESO EN EJECUCION        
                DECLARE @V_ERR_PARAMETROS VARCHAR(MAX) = '';
                DECLARE @V_ERR_ID_ORIGEN INT = @V_ID_ORIGEN_PROCEDIMIENTO;
                DECLARE @V_ERR_DESCRIPCION_ORIGEN VARCHAR(300) = @V_DESCRIPCION_ORIGEN_PROCEDIMIENTO; --1=WINDOWS FORM,2=WEB SERVICE,3=WEB SITE,4=PROCEDURE,5=REPORTING SERVICE,6=ETL
                DECLARE @V_ERR_ID_TIPO INT = @V_ID_TIPO_ERROR;
                DECLARE @V_ERR_DESCRIPCION_TIPO VARCHAR(300) = @V_DESCRIPCION_TIPO_ERROR; --1=ERROR,2=EXCEPCION-VALIDACION
                DECLARE @V_ERR_DETALLE VARCHAR(500) = '';
                DECLARE @V_ERR_MENSAJE_USUARIO VARCHAR(MAX) = '';
                DECLARE @V_ERR_MENSAJE_SISTEMA VARCHAR(MAX) = '';
                DECLARE @V_ERR_OBSERVACION VARCHAR(MAX) = '';
                DECLARE @V_ERR_OTROS_DATOS VARCHAR(MAX) = '';
                DECLARE @V_ERR_XML_DATOS VARCHAR(MAX) = '';
                DECLARE @V_ERR_DESCRIPCION_TIPO_DATOS VARCHAR(300) = '';
                DECLARE @V_ERR_ID_TIPO_DATOS SMALLINT = 0;
                DECLARE @V_ERR_NUMERO_USUARIO INT = 75000;
                DECLARE @V_ERR_NUMERO_SISTEMA INT = 0;
                DECLARE @V_ERR_NIVEL_ANIDAMIENTO INT = ISNULL(@@NESTLEVEL, 0);
                DECLARE @V_ERR_APLICATIVO VARCHAR(50) = '';
                SELECT TOP 1
                    @V_ERR_APLICATIVO
                        = CONCAT(
                                    'Host:',
                                    ISNULL(vs.host_name, 'N/D'),
                                    ' | Program:',
                                    ISNULL(vs.program_name, 'N,/D'),
                                    ' | Login:',
                                    ISNULL(vs.login_name, 'N,/D')
                                )
                FROM sys.dm_exec_requests vr
                    INNER JOIN sys.dm_exec_sessions vs
                        ON vr.session_id = vs.session_id
                WHERE vr.session_id = @V_ERR_ID_SPID;
                SELECT @V_STR_ESQUEMA = SCHEMA_NAME(schema_id)
                FROM sys.procedures
                WHERE (
                          object_id = @@PROCID
                          OR object_id = @@SPID
                      );
                SELECT @V_ERR_PROCESO = CONCAT(@V_STR_ESQUEMA, '.', @V_ERR_PROCESO);
                DECLARE @V_ERR_NUMERO_LINEA INT = 0;
                DECLARE @V_ERR_SENTENCIA VARCHAR(MAX) = '';
                DECLARE @VBI_ERR_DATOS VARBINARY(MAX) = NULL;
                DECLARE @V_EJEC_DESCRIPCION_TIPO_DATOS VARCHAR(300) = '';
                DECLARE @V_EJEC_ID_TIPO_DATOS SMALLINT = 0;
                DECLARE @V_BIN_EJEC_FK_UTL_TRA_BITACORA_EJECUCION BIGINT = 0;
                DECLARE @V_BIT_EJEC_ACTIVO_EJECUCION INT = 1; --UTILIZA COMO INDICADOR DE QUE EL PROCEDIMIENTO EJECUTA EL CUERPO DEL MISMO O NO  
                DECLARE @V_OBSERVACIONES VARCHAR(MAX) = '';
            END; --INICIO-ADMINISTRACION ERRORES
            BEGIN --INICIO-VARIOS
                DECLARE @V_CANTIDAD_REGISTROS INT = 0;
                DECLARE @V_BIT_REGISTRA_XML_PROCESOS_SISTEMA BIT = 0;
                --SI NO VIENE NINGUN VALOR SE GENERA UNO                                    
                SELECT @P_IDENTIFICADOR_EXTERNO = CASE
                                                      WHEN LTRIM(RTRIM(@P_IDENTIFICADOR_EXTERNO)) = '' THEN
                                                          CONVERT(VARCHAR(50), NEWID())
                                                      ELSE
                                                          @P_IDENTIFICADOR_EXTERNO
                                                  END;
            END; --FIN-VARIOS  


            --BEGIN--INICIO-VARIABLE O CONSTANTES DEL CUERPO DEL PROCEDIMIENTO 
			
            DECLARE @SET_SQLSTRING NVARCHAR(MAX);
            DECLARE @PARAM_DEFINITION NVARCHAR(MAX);
		

        --END--FIN-VARIABLE O CONSTANTES DEL CUERPO DEL PROCEDIMIENTO


        END; --FIN-DECLARACION VARIABLES Y CONSTANTES                                           





        --++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++  
        -----------------------------                                    
        --CUERPO PROCEDIMIENTO                                    
        -----------------------------       
        BEGIN --INICIO-CUERPO PROCEDIMIENTO
            IF (@P_MODO_EJECUCION = @V_MODO_EJECUCION_DEPURACION)
            BEGIN
                SELECT BANDERA_EJECUCION = CONCAT('00000-', @V_ERR_PROCESO),
                       FECHA = GETDATE(),
                       STR_USUARIO = @P_USUARIO,
                       SIN_MODO_EJECUCION = @P_MODO_EJECUCION,
                       SIN_OPCION = @P_OPCION;
                SELECT BANDERA_EJECUCION = CONCAT('00010-', @V_ERR_PROCESO),
                       FECHA = GETDATE(),
                       SENTENCIA_EJECUCION = @V_ERR_SENTENCIA;
            END;
            --------------------------------------------------------------------------------------------------------------------------------
            --BEGIN--INICIO-INICIAL REGISTRO DE BITACORAS DE EJECUCION
            IF (@P_MODO_EJECUCION = @V_MODO_EJECUCION_DEPURACION)
            BEGIN
                SELECT BANDERA_EJECUCION = CONCAT('00020-', @V_ERR_PROCESO),
                       FECHA = GETDATE(),
                       BIT_EJEC_ACTIVO_EJECUCION = @V_BIT_EJEC_ACTIVO_EJECUCION,
                       BIN_EJEC_FK_UTL_TRA_BITACORA_EJECUCION = @V_BIN_EJEC_FK_UTL_TRA_BITACORA_EJECUCION;
            END;
            IF (@V_BIT_EJEC_ACTIVO_EJECUCION = 1)
            BEGIN

                IF (@P_OPCION = 0)
                BEGIN
                    SELECT @V_ERR_NUMERO_USUARIO = 75001;
                    SELECT @V_ERR_MENSAJE_USUARIO = 'ERROR AL EJECUTAR LA CONSULTA DE LA TABLA TBL_SIMP_CLIENTE';

				SELECT	@SET_SQLSTRING =
				N'SELECT [PK_TBL_SIMP_CLIENTE]
					  ,[NOMBRE]
					  ,[PRIMER_APELLIDO]
					  ,[SEGUNDO_APELLIDO]
					  ,[CORREO_ELECTRONICO]
					  ,[TELEFONO]
				  FROM ' + @P_ESQUEMA + N'.[TBL_SIMP_CLIENTE]
				  ORDER BY PK_TBL_SIMP_CLIENTE asc'

				  SELECT @PARAM_DEFINITION
					= '@P_ID bigint,
					   @P_NOMBRE varchar(50),
					   @P_PRIMER_APELLIDO varchar(50),
					   @P_SEGUNDO_APELLIDO varchar(50),
                       @P_CORREO_ELECTRONICO varchar(50),
					   @P_TELEFONO varchar(15),
					   @P_USUARIO varchar(50),
					   @P_ESQUEMA varchar(50)'
					     
				   EXEC sys.sp_executesql @SET_SQLSTRING,
				   		@PARAM_DEFINITION,
						@P_ID = @P_ID,
						@P_NOMBRE = @P_NOMBRE,
						@P_PRIMER_APELLIDO = @P_PRIMER_APELLIDO,
						@P_SEGUNDO_APELLIDO = @P_SEGUNDO_APELLIDO,
                        @P_CORREO_ELECTRONICO = @P_CORREO_ELECTRONICO,
						@P_TELEFONO = @P_TELEFONO,
						@P_USUARIO = @P_USUARIO,
						@P_ESQUEMA = @P_ESQUEMA

                END;

				 IF (@P_OPCION = 1)
                BEGIN
                    SELECT @V_ERR_NUMERO_USUARIO = 75001;
                    SELECT @V_ERR_MENSAJE_USUARIO = 'ERROR AL EJECUTAR LA CONSULTA DE LA TABLA TBL_SIMP_CLIENTE';

				SELECT	@SET_SQLSTRING =
				N'SELECT [PK_TBL_SIMP_CLIENTE]
					  ,[NOMBRE]
					  ,[PRIMER_APELLIDO]
					  ,[SEGUNDO_APELLIDO]
					  ,[CORREO_ELECTRONICO]
					  ,[TELEFONO]
				  FROM ' + @P_ESQUEMA + N'.[TBL_SIMP_CLIENTE] WHERE  ([PK_TBL_SIMP_CLIENTE] =  @P_ID OR @P_ID = 0) 
				  AND ([NOMBRE] = @P_NOMBRE OR @P_NOMBRE = '''')
				  AND ([PRIMER_APELLIDO] = @P_PRIMER_APELLIDO OR @P_PRIMER_APELLIDO = '''')
				  AND ([SEGUNDO_APELLIDO] = @P_SEGUNDO_APELLIDO OR @P_SEGUNDO_APELLIDO = '''')
				  AND ([CORREO_ELECTRONICO] = @P_CORREO_ELECTRONICO OR @P_CORREO_ELECTRONICO = '''')
                  AND ([TELEFONO] = @P_TELEFONO OR @P_TELEFONO = '''')'
						
						SELECT @PARAM_DEFINITION
					= '@P_ID bigint,
					   @P_NOMBRE varchar(50),
					   @P_PRIMER_APELLIDO varchar(50),
					   @P_SEGUNDO_APELLIDO varchar(50),
                       @P_CORREO_ELECTRONICO varchar(50),
					   @P_TELEFONO varchar(15),
					   @P_USUARIO varchar(50),
					   @P_ESQUEMA varchar(50)'
		
  
				   EXEC sys.sp_executesql @SET_SQLSTRING,
				   		@PARAM_DEFINITION,
						@P_ID = @P_ID,
						@P_NOMBRE = @P_NOMBRE,
						@P_PRIMER_APELLIDO = @P_PRIMER_APELLIDO,
						@P_SEGUNDO_APELLIDO = @P_SEGUNDO_APELLIDO,
                        @P_CORREO_ELECTRONICO = @P_CORREO_ELECTRONICO,
						@P_TELEFONO = @P_TELEFONO,
						@P_USUARIO = @P_USUARIO,
						@P_ESQUEMA = @P_ESQUEMA

                END;
                  

            -- A MODIFICAR
            END;
        --------------------------------------------------------------------------------------------------------------------------------
        --BEGIN--INICIO-INICIAL REGISTRO DE BITACORAS DE EJECUCION
        --END--FIN-INICIAL REGISTRO DE BITACORAS DE EJECUCION


        --/*MODIFICAR EN CASO DE SELECT*/
        --COMMIT TRANSACTION;

        END; --FIN-CUERPO PROCEDIMIENTO              
    --+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++                                    
    END TRY
    BEGIN CATCH
        -----------------------------                                    
        --ADMINISTRACION DE ERRORES                                  
        -----------------------------     
        -- A MODIFICAR
       SELECT @V_ERR_PARAMETROS = CONCAT(@V_ERR_PARAMETROS, '@P_OPCION=', ISNULL(@P_OPCION, -1));
        SELECT @V_ERR_PARAMETROS = CONCAT(@V_ERR_PARAMETROS, ',@P_USUARIO=', '''', ISNULL(@P_USUARIO, 'N/D'), '''');
		SELECT @V_ERR_PARAMETROS = CONCAT(@V_ERR_PARAMETROS, ',@P_NOMBRE=', '''', ISNULL(@P_NOMBRE, -1));
		SELECT @V_ERR_PARAMETROS = CONCAT(@V_ERR_PARAMETROS, ',@P_PRIMER_APELLIDO=', '''', ISNULL(@P_PRIMER_APELLIDO, 'N/D'), '''');
		SELECT @V_ERR_PARAMETROS = CONCAT(@V_ERR_PARAMETROS, ',@P_SEGUNDO_APELLIDO=', '''', ISNULL(@P_SEGUNDO_APELLIDO, 'N/D'), '''');
        SELECT @V_ERR_PARAMETROS = CONCAT(@V_ERR_PARAMETROS, ',@P_CORREO_ELECTRONICO=', '''', ISNULL(@P_CORREO_ELECTRONICO, 'N/D'), '''');
        SELECT @V_ERR_PARAMETROS = CONCAT(@V_ERR_PARAMETROS, ',@P_TELEFONO=', '''', ISNULL(@P_TELEFONO, 'N/D'), '''');
        SELECT @V_ERR_PARAMETROS = CONCAT(@V_ERR_PARAMETROS, ',@P_ESQUEMA=', '''', ISNULL(@P_ESQUEMA, 'N/D'), '''');

        -- A MODIFICAR
        SELECT @V_ERR_SENTENCIA = CONCAT('EXEC ', @V_STR_ESQUEMA, '.', @V_ERR_PROCESO, ' ', @V_ERR_PARAMETROS);
        BEGIN --INICIO-ADMINISTRACION DE ERRORES
            --IF (@P_MODO_EJECUCION = @V_MODO_EJECUCION_DEPURACION)
            --BEGIN
            --    SELECT BANDERA_EJECUCION = CONCAT('ERROR-99999-01-', @V_ERR_PROCESO),
            --           FECHA = GETDATE(),
            --           [XACT_STATE] = XACT_STATE(),
            --           TRANCOUNT = @@TRANCOUNT;
            --END;
            -- --VALIDA SI LA TRANSACCION NO SE PUEDE HACER COMMIT
            --IF (XACT_STATE()) = -1
            --BEGIN
            --    IF (@@TRANCOUNT > 0)
            --    BEGIN
            --        ROLLBACK TRANSACTION;
            --    END;
            --END;
            ---- VALIDA SI LA TRANSACCION SE PUEDE HACER COMMIT
            --IF (XACT_STATE()) = 1
            --BEGIN
            --    IF (@@TRANCOUNT > 0)
            --    BEGIN
            --        COMMIT TRANSACTION;
            --    END;
            --END;
            ERRORHANDLER:
            SELECT @V_ERR_SEVERIDAD = ISNULL(ERROR_SEVERITY(), 0);
            SELECT @V_ERR_ESTADO = ISNULL(ERROR_STATE(), 0);
            SELECT @V_ERR_NUMERO_LINEA = ISNULL(ERROR_LINE(), 0);
            SELECT @V_ERR_MENSAJE_SISTEMA = ISNULL(ERROR_MESSAGE(), '');
            SELECT @V_ERR_NUMERO_SISTEMA = ISNULL(ERROR_NUMBER(), 0);
            --DEFINICION DE MENSAJE DE ERROR
            SELECT @V_ERR_OBSERVACION
                = '(' + CONVERT(VARCHAR, @V_ERR_NIVEL_ANIDAMIENTO) + ')' + +',MENSAJE:'
                  + ISNULL(@V_ERR_MENSAJE_USUARIO, 'N/D') + '|' + ISNULL(@V_ERR_MENSAJE_SISTEMA, 'N/D') + ',PROCESO:'
                  + ISNULL(@V_ERR_PROCESO, 'N/D');
            BEGIN TRY
                SELECT @V_FEC_ERR_CREACION = GETDATE();
                EXEC dbo.PA_MAN_TBL_TRA_EXCEPCION_GUARDAR @SIN_MODO_EJECUCION = @P_MODO_EJECUCION,                     -- smallint
                                                            @SIN_FK_UTL_PAR_PAIS = @V_ERR_FK_UTL_PAR_PAIS,               -- int
                                                            @STR_USUARIO_CREACION = @V_ERR_USUARIO_CREACION,             -- varchar(50)
                                                            @FEC_CREACION = @V_FEC_ERR_CREACION,                         -- datetime
                                                            @STR_USUARIO_MODIFICACION = '',                              -- varchar(50)
                                                            @FEC_MODIFICACION = '19000101',                              -- datetime
                                                            @INT_ID_SPID = @V_ERR_ID_SPID,                               -- int
                                                            @FEC_SISTEMA = @V_FEC_ERR_SISTEMA,                           -- datetime
                                                            @INT_PRIORIDAD = @V_ERR_PRIORIDAD,                           -- int
                                                            @INT_SEVERIDAD = @V_ERR_SEVERIDAD,                           -- int
                                                            @INT_ERROR_ESTADO = @V_ERR_ESTADO,                           -- int
                                                            @STR_SERVIDOR = @V_ERR_SERVIDOR,                             -- varchar(300)
                                                            @STR_ESTACION = @V_STR_ESQUEMA,                              -- varchar(300)
                                                            @STR_BASE_DATOS = @V_ERR_BASE_DATOS,                         -- varchar(300)
                                                            @STR_APLICATIVO = @V_ERR_APLICATIVO,                         -- varchar(300)
                                                            @STR_CLASE = '',                                             -- varchar(300)
                                                            @STR_EVENTO = '',                                            -- varchar(300)
                                                            @STR_PROCESO = @V_ERR_PROCESO,                               -- varchar(300)
                                                            @STR_PARAMETROS = @V_ERR_PARAMETROS,                         -- varchar(max)
                                                            @INT_ID_ORIGEN = @V_ERR_ID_ORIGEN,                           -- int
                                                            @STR_DESCRIPCION_ORIGEN = @V_ERR_DESCRIPCION_ORIGEN,         -- varchar(300)
                                                            @INT_ID_TIPO = @V_ERR_ID_TIPO,                               -- int
                                                            @STR_DESCRIPCION_TIPO = @V_ERR_DESCRIPCION_TIPO,             -- varchar(300)
                                                            @STR_DETALLE = @V_ERR_DETALLE,                               -- varchar(500)
                                                            @STR_MENSAJE = @V_ERR_MENSAJE_USUARIO,                       -- varchar(max)
                                                            @STR_MENSAJE_SISTEMA = @V_ERR_MENSAJE_SISTEMA,               -- varchar(max)
                                                            @STR_OBSERVACION = @V_ERR_OBSERVACION,                       -- varchar(max)
                                                            @STR_OTROS_DATOS = @V_ERR_OTROS_DATOS,                       -- varchar(max)
                                                            @SIN_ID_TIPO_DATOS = @V_ERR_ID_TIPO_DATOS,                   -- smallint
                                                            @VBI_DATOS = @VBI_ERR_DATOS,                                 -- varbinary(max)
                                                            @STR_DESCRIPCION_TIPO_DATOS = @V_ERR_DESCRIPCION_TIPO_DATOS, -- varchar(300)
                                                            @STR_SENTENCIA = @V_ERR_SENTENCIA,                           -- varchar(max)
                                                            @INT_NUMERO_USUARIO = @V_ERR_NUMERO_USUARIO,                 -- int
                                                            @INT_NUMERO_SISTEMA = @V_ERR_NUMERO_SISTEMA,                 -- int
                                                            @INT_NIVEL_ANIDAMIENTO = @V_ERR_NIVEL_ANIDAMIENTO,           -- int
                                                            @INT_NUMERO_LINEA = @V_ERR_NUMERO_LINEA,                     -- int
                                                            @STR_IDENTIFICADOR_EXTERNO = @P_IDENTIFICADOR_EXTERNO,       -- varchar(50)
                                                            @STR_IDENTIFICADOR_UNICO = @V_IDENTIFICADOR_UNICO,           -- varchar(50)
															@STR_COMPANNIA=@P_ESQUEMA
            END TRY
            BEGIN CATCH
                IF (@P_MODO_EJECUCION = @V_MODO_EJECUCION_DEPURACION)
                BEGIN
                    SELECT BANDERA_EJECUCION = CONCAT('ERROR-99999-02-', @V_ERR_PROCESO),
                           FECHA = GETDATE();
                END;
                SELECT @V_ERR_SEVERIDAD = ISNULL(ERROR_SEVERITY(), 0);
                SELECT @V_ERR_ESTADO = ISNULL(ERROR_STATE(), 0);
                SELECT @V_ERR_NUMERO_LINEA = ISNULL(ERROR_LINE(), 0);
                SELECT @V_ERR_MENSAJE_SISTEMA = ISNULL(ERROR_MESSAGE(), '');
                SELECT @V_ERR_NUMERO_SISTEMA = ISNULL(ERROR_NUMBER(), 0);
                --DEFINICION DE MENSAJE DE ERROR
                SELECT @V_ERR_NUMERO_USUARIO = 76001;
                --VERBOS A UTILIZAR DE ACUERDO A LA OPERACION=SELECT:SELECCIONAR, DELETE: ELIMINAR, UPDATE: ACTUALIZAR, INSERT: INSERTAR, EXEC: EJECUTAR
                SELECT @V_ERR_MENSAJE_USUARIO = 'ERROR AL EJECUTAR EL PROCEDIMIENTO <PA_MAN_UTL_TRA_EXCEPCION_GUARDAR>';
                SELECT @V_ERR_OBSERVACION
                    = '(' + CONVERT(VARCHAR, @V_ERR_NIVEL_ANIDAMIENTO) + ')' + +',MENSAJE:'
                      + ISNULL(@V_ERR_MENSAJE_USUARIO, 'N/D') + '|' + ISNULL(@V_ERR_MENSAJE_SISTEMA, 'N/D')
                      + ',PROCESO:' + ISNULL(@V_ERR_PROCESO, 'N/D');
                RAISERROR(@V_ERR_OBSERVACION, 16, 1);
            END CATCH;
            RAISERROR(@V_ERR_OBSERVACION, 16, 1);
            RETURN @V_ERR_NUMERO_USUARIO;
        END; --FIN-ADMINISTRACION DE ERRORES
    END CATCH;
    --++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++      
    --COMMIT TRAN   
    RETURN 0;
END;


