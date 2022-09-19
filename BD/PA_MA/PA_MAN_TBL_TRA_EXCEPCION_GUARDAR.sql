
CREATE PROC [dbo].[PA_MAN_TBL_TRA_EXCEPCION_GUARDAR]
    @SIN_MODO_EJECUCION SMALLINT = 0,
    @SIN_FK_UTL_PAR_PAIS INT = 0,
    @STR_USUARIO_CREACION VARCHAR(50) = '',
    @FEC_CREACION DATETIME = '19000101',
    @STR_USUARIO_MODIFICACION VARCHAR(50) = '',
    @FEC_MODIFICACION DATETIME = '19000101',
    @INT_ID_SPID INT = 0,
    @FEC_SISTEMA DATETIME = '19000101',
    @INT_PRIORIDAD INT = 0,
    @INT_SEVERIDAD INT = 0,
    @INT_ERROR_ESTADO INT = 0,
    @STR_SERVIDOR VARCHAR(300) = '',
    @STR_ESTACION VARCHAR(300) = '',
    @STR_BASE_DATOS VARCHAR(300) = '',
    @STR_APLICATIVO VARCHAR(300) = '',
    @STR_CLASE VARCHAR(300) = '',
    @STR_EVENTO VARCHAR(300) = '',
    @STR_PROCESO VARCHAR(300) = '',
    @STR_PARAMETROS VARCHAR(MAX) = '',
    @INT_ID_ORIGEN INT = 0,                    --1=WINDOWS FORM,2=WEB SERVICE,3=WEB SITE,4=PROCEDURE,5=REPORTING SERVICE,6=ETL 
    @STR_DESCRIPCION_ORIGEN VARCHAR(300) = '', --WINDOWS FORM WEB SERVICE WEB SITE PROCEDURE REPORTING SERVICE ETL 
    @INT_ID_TIPO INT = 0,
    @STR_DESCRIPCION_TIPO VARCHAR(300) = '',   --ERROREXCEPCION-VALIDACION 
    @STR_DETALLE VARCHAR(500) = '',
    @STR_MENSAJE VARCHAR(MAX) = '',
    @STR_MENSAJE_SISTEMA VARCHAR(MAX) = '',
    @STR_OBSERVACION VARCHAR(MAX) = '',
    @STR_OTROS_DATOS VARCHAR(MAX) = '',
    @SIN_ID_TIPO_DATOS SMALLINT = 0,
    @VBI_DATOS VARBINARY(MAX) = NULL,
    @STR_DESCRIPCION_TIPO_DATOS VARCHAR(300) = '',
    @STR_SENTENCIA VARCHAR(MAX) = '',
    @INT_NUMERO_USUARIO INT = 0,
    @INT_NUMERO_SISTEMA INT = 0,
    @INT_NIVEL_ANIDAMIENTO INT = 0,
    @INT_NUMERO_LINEA INT = 0,
    @STR_IDENTIFICADOR_EXTERNO VARCHAR(50) = '',
    @STR_IDENTIFICADOR_UNICO VARCHAR(50) = '' OUTPUT,
    @STR_COMPANNIA VARCHAR(50) = ''
AS /*  
 
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
COMENTARIOS: 
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
-->>>CREACION: 
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
OBJETO:			[PA_MAN_TBL_TRA_EXCEPCION_GUARDAR] 
DESCRIPCION:	PROCEDIMIENTO ENCARGADO DE REALIZAR LA INSERCION DE ERRORES O EXCEPCIONES EN LA TABLA T_UTLTEXCEPCION 
CREADO POR: 	Oscar.Ocampo
VERSION:    	1.0.1 
FECHA:  		05/12/2018
OBSERVACIONES: 
1. SI SE DESEA REGISTRAR EL ERROR EN UN LUGAR CENTRALIZADO, COLOCARLO DESPUES DEL INGRESO LOCAL, SIN MANEJO TRANSACCIONAL, POR SI SE CAE REGISTRE AL MENOS LOCALMENTE 
 
EJM EJECUCION: 
 
 
 
SELECT * FROM TBL_TRA_EXCEPCION_GUARDAR
 
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
-->>>MODIFICACIONES: 
(POR CADA MODIFICACION SE DEBE DE AGREGAR UN ITEM, CON SU RESPECTIVA  INFORMACION, FAVOR QUE SEA DE FORMA ASCENDENTE CON RESPECTO A LAS  
MODIFICACIONES, ADEMAS DE IDENTIFICAR EN EL CODIGO DEL PROCEDIMIENTO EL CAMBIO REALIZADO, DE LA SIGUIENTE FORMA 
--NUMERO MODIFICACION: ##, MODIFICADO POR: DESARROLLADOR, OBSERVACIONES: DESCRIPCION A DETALLE DEL CAMBIO) 
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
NUMERO MODIFICACION: 	01 
DESCRIPCION: 			00000-00 FIX,SE COMENTA LA SECCION DE ALMACENAR EN LA TABLA  
						UTL_TRA_BITACORA_EJECUCION_BINARIOS, GENERA ESPACIO Y NO SE UTILIZA 
USO: 					 
MODIFICADO POR: 		XXXXXX XXXXXX 
VERSION: 				1.0.2 
EJEMPLO DE USO: 		 
FECHA: 					YYYY/MM/DD
OBSERVACIONES: 									 
------------------------------------------------------------------------------------------------------- 
NUMERO MODIFICACION: 	NUMERO DE SECUENCIA DE LA MODIFICACION, COMENSAR DE 1,2,3,...N, EN ADELANTE 
DESCRIPCION: 			DESCRIPCION DE LA MODIFICACION REALIZADA 
USO: 					SI ALGO CAMBIO EN LA FORMA DE LA EJECUCION, SE COLOCA, SI NO COLOCAR "IGUAL AL ITEN DE CREACION" 
MODIFICADO POR: 		DESARROLLADOR 
VERSION: 				1.1.0 O 1.1.1, ESTO TOMANDO COMO BASE LA VERSION DE LA CREACION, ULTIMA MODIFICACION, Y EL TIPO DE CAMBIO REALIZADO EN EL PROCEDIMIENTO 
EJEMPLO DE USO: 		SI ALGO CAMBIO EN LA FORMA DE LA EJECUCION, SE COLOCA, SI NO COLOCAR "IGUAL AL ITEN DE CREACION" 
FECHA: 					FECHA DE LA MODIFICACION 
OBSERVACIONES: 			POSIBLES OBSERVACIONES 
-------------------------------------------------------------------------------------------------------  
*/
BEGIN


    SET XACT_ABORT, NOCOUNT ON; --ADDED TO PREVENT EXTRA RESULT SETS FROM INTERFERING WITH SELECT STATEMENTS. 
    --SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED; -- ADD NOLOCK ALL TABLES  

    BEGIN TRY
        --BEGIN TRANSACTION   

        --++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++  
        -----------------------------                                     
        --DECLARACION Y ASIGNACION DE VARIABLES                                   
        ----------------------------- 
        BEGIN --INICIO-DECLARACION Y ASIGNACION DE VARIABLES 
            BEGIN --INICIO-CONSTANTES 
                DECLARE @SIN_MODO_EJECUCION_NORMAL SMALLINT = 0;
                DECLARE @SIN_MODO_EJECUCION_DEPURACION SMALLINT = 7;

                --1=WINDOWS FORM,2=WEB SERVICE,3=WEB SITE,4=PROCEDURE,5=REPORTING SERVICE,6=ETL 
                DECLARE @INT_ID_ORIGEN_WINDOWS_FORM INT = 1;
                DECLARE @STR_DESCRIPCION_ORIGEN_WINDOWS_FORM VARCHAR(300) = 'WINDOWS FORM';

                DECLARE @INT_ID_ORIGEN_WEB_SERVICE INT = 2;
                DECLARE @STR_DESCRIPCION_ORIGEN_WEB_SERVICE VARCHAR(300) = 'WEB SERVICE';

                DECLARE @INT_ID_ORIGEN_WEB_SITE INT = 3;
                DECLARE @STR_DESCRIPCION_ORIGEN_WEB_SITE VARCHAR(300) = 'WEB SITE';

                DECLARE @INT_ID_ORIGEN_PROCEDIMIENTO INT = 4;
                DECLARE @STR_DESCRIPCION_ORIGEN_PROCEDIMIENTO VARCHAR(300) = 'PROCEDIMIENTO ALMACENADO';

                DECLARE @INT_ID_ORIGEN_REPORTING_SERVICE INT = 5;
                DECLARE @STR_DESCRIPCION_ORIGEN_REPORTING_SERVICE VARCHAR(300) = 'REPORTING SERVICE';

                DECLARE @INT_ID_ORIGEN_ETL INT = 6;
                DECLARE @STR_DESCRIPCION_ETL VARCHAR(300) = 'ETL';



                DECLARE @SIN_ID_TIPO_DATOS_XML SMALLINT = 1; --ID Tipo dato XML 

            END; --FIN-CONSTANTES 

            BEGIN --INICIO-ADMINISTRACION ERRORES 
                DECLARE @STR_ERROR_PARAMETROS VARCHAR(MAX) = '';
                DECLARE @INT_ERROR_NUMERO_USUARIO INT = 75000;
                DECLARE @STR_ERROR_MENSAJE VARCHAR(MAX) = '';
                DECLARE @STR_ERROR_PROCESO [VARCHAR](300) = OBJECT_NAME(@@PROCID);
                DECLARE @STR_ERROR_OBSERVACION VARCHAR(MAX) = '';
                DECLARE @STR_ERROR_SENTENCIA VARCHAR(MAX) = '';
            END; --FINAL-ADMINISTRACION ERRORES 

            BEGIN --INICIO-VARIOS 
                DECLARE @BIN_PK_TBL_TRA_EXCEPCION BIGINT = 0;
                DECLARE @XML_DATOS XML = NULL;
                DECLARE @BIN_REGISTRA_XML_PROCESOS_SISTEMA_GLOBAL BIGINT = 0;
                DECLARE @BIN_REGISTRA_XML_PROCESOS_SISTEMA_GLOBAL_MODULO BIGINT = 0;
                DECLARE @BIN_REGISTRA_XML_PROCESOS_SISTEMA BIGINT = 0;

                DECLARE @STR_LLAVE_01_UTL_PAR_GENERALES VARCHAR(300) = '';
                DECLARE @STR_LLAVE_02_UTL_PAR_GENERALES VARCHAR(300) = '';
                DECLARE @STR_LLAVE_03_UTL_PAR_GENERALES VARCHAR(300) = '';
                DECLARE @INT_CANTIDAD_REGISTROS INT = 0;

                SELECT @SIN_MODO_EJECUCION = ISNULL(@SIN_MODO_EJECUCION, -1);
                SELECT @SIN_FK_UTL_PAR_PAIS = ISNULL(@SIN_FK_UTL_PAR_PAIS, -1);
                SELECT @STR_USUARIO_CREACION = ISNULL(@STR_USUARIO_CREACION, 'N/D');
                SELECT @FEC_CREACION = ISNULL(@FEC_CREACION, '19000101');
                SELECT @STR_USUARIO_MODIFICACION = ISNULL(@STR_USUARIO_MODIFICACION, 'N/D');
                SELECT @FEC_MODIFICACION = ISNULL(@FEC_MODIFICACION, '19000101');
                SELECT @INT_ID_SPID = ISNULL(@INT_ID_SPID, -1);
                SELECT @FEC_SISTEMA = ISNULL(@FEC_SISTEMA, '19000101');
                SELECT @INT_PRIORIDAD = ISNULL(@INT_PRIORIDAD, -1);
                SELECT @INT_SEVERIDAD = ISNULL(@INT_SEVERIDAD, -1);
                SELECT @INT_ERROR_ESTADO = ISNULL(@INT_ERROR_ESTADO, -1);
                SELECT @STR_SERVIDOR = ISNULL(@STR_SERVIDOR, 'N/D');
                SELECT @STR_ESTACION = ISNULL(@STR_ESTACION, 'N/D');
                SELECT @STR_BASE_DATOS = ISNULL(@STR_BASE_DATOS, 'N/D');
                SELECT @STR_APLICATIVO = ISNULL(@STR_APLICATIVO, 'N/D');
                SELECT @STR_CLASE = ISNULL(@STR_CLASE, 'N/D');
                SELECT @STR_EVENTO = ISNULL(@STR_EVENTO, 'N/D');
                SELECT @STR_PROCESO = ISNULL(@STR_PROCESO, 'N/D');
                SELECT @STR_PARAMETROS = ISNULL(@STR_PARAMETROS, 'N/D');
                SELECT @INT_ID_ORIGEN = ISNULL(@INT_ID_ORIGEN, -1);
                SELECT @STR_DESCRIPCION_ORIGEN = ISNULL(@STR_DESCRIPCION_ORIGEN, 'N/D');
                SELECT @INT_ID_TIPO = ISNULL(@INT_ID_TIPO, -1);
                SELECT @STR_DESCRIPCION_TIPO = ISNULL(@STR_DESCRIPCION_TIPO, 'N/D');
                SELECT @STR_DETALLE = ISNULL(@STR_DETALLE, 'N/D');
                SELECT @STR_MENSAJE = ISNULL(@STR_MENSAJE, 'N/D');
                SELECT @STR_MENSAJE_SISTEMA = ISNULL(@STR_MENSAJE_SISTEMA, 'N/D');
                SELECT @STR_OBSERVACION = ISNULL(@STR_OBSERVACION, 'N/D');

                SELECT @STR_OTROS_DATOS = ISNULL(@STR_OTROS_DATOS, 'N/D');

                SELECT @SIN_ID_TIPO_DATOS = ISNULL(@SIN_ID_TIPO_DATOS, -1);

                SELECT @STR_DESCRIPCION_TIPO_DATOS = ISNULL(@STR_DESCRIPCION_TIPO_DATOS, 'N/D');

                SELECT @STR_SENTENCIA = ISNULL(@STR_SENTENCIA, 'N/D');

                SELECT @INT_NUMERO_USUARIO = ISNULL(@INT_NUMERO_USUARIO, -1);
                SELECT @INT_NUMERO_SISTEMA = ISNULL(@INT_NUMERO_SISTEMA, -1);
                SELECT @INT_NIVEL_ANIDAMIENTO = ISNULL(@INT_NIVEL_ANIDAMIENTO, -1);
                SELECT @INT_NUMERO_LINEA = ISNULL(@INT_NUMERO_LINEA, -1);

                DECLARE @UNQ_IDENTIFICADOR_UNICO UNIQUEIDENTIFIER = NEWID();
                --CASE 
                --                                             WHEN LTRIM(RTRIM(@STR_IDENTIFICADOR_UNICO)) = '' 
                --                                             THEN NEWID() 
                --                                             ELSE CONVERT(UNIQUEIDENTIFIER, @STR_IDENTIFICADOR_UNICO) 
                --                                             END  

                SELECT @STR_IDENTIFICADOR_UNICO = CONVERT(VARCHAR(50), @UNQ_IDENTIFICADOR_UNICO);

                SELECT @STR_ERROR_PARAMETROS
                    = CONCAT(
                                '@SIN_MODO_EJECUCION=',
                                ISNULL(@SIN_MODO_EJECUCION, -1),
                                ',@SIN_FK_UTL_PAR_PAIS=',
                                ISNULL(@SIN_FK_UTL_PAR_PAIS, -1),
                                ',@STR_USUARIO_CREACION=',
                                '''',
                                ISNULL(@STR_USUARIO_CREACION, 'N/D'),
                                '''',
                                ',@FEC_CREACION=''',
                                ISNULL(@FEC_CREACION, 19000101),
                                '''',
                                ',@STR_USUARIO_MODIFICACION=',
                                '''',
                                ISNULL(@STR_USUARIO_MODIFICACION, 'N/D'),
                                '''',
                                ',@FEC_MODIFICACION=',
                                '''',
                                ISNULL(@FEC_MODIFICACION, 19000101),
                                '''',
                                ',@INT_ID_SPID=',
                                ISNULL(@INT_ID_SPID, -1),
                                ',@FEC_SISTEMA=',
                                '''',
                                ISNULL(@FEC_SISTEMA, 19000101),
                                '''',
                                ',@INT_PRIORIDAD=',
                                ISNULL(@INT_PRIORIDAD, -1),
                                ',@INT_SEVERIDAD=',
                                ISNULL(@INT_SEVERIDAD, -1),
                                ',@INT_ERROR_ESTADO=',
                                ISNULL(@INT_ERROR_ESTADO, -1),
                                ',@STR_SERVIDOR=',
                                '''',
                                ISNULL(@STR_SERVIDOR, 'N/D'),
                                '''',
                                ',@STR_ESTACION=',
                                '''',
                                ISNULL(@STR_ESTACION, 'N/D'),
                                '''',
                                ',@STR_BASE_DATOS=',
                                '''',
                                ISNULL(@STR_BASE_DATOS, 'N/D'),
                                ',@STR_APLICATIVO=',
                                '''',
                                ISNULL(@STR_APLICATIVO, 'N/D'),
                                '''',
                                ',@STR_CLASE=',
                                '''',
                                ISNULL(@STR_CLASE, 'N/D'),
                                '''',
                                ',@STR_EVENTO=',
                                '''',
                                ISNULL(@STR_EVENTO, 'N/D'),
                                '''',
                                ',@STR_PROCESO=',
                                '''',
                                ISNULL(@STR_PROCESO, 'N/D'),
                                '''',
                                ',@STR_PARAMETROS=',
                                '''',
                                ISNULL(@STR_PARAMETROS, 'N/D'),
                                '''',
                                ',@INT_ID_ORIGEN=',
                                ISNULL(@INT_ID_ORIGEN, -1),
                                ',@STR_DESCRIPCION_ORIGEN=',
                                '''',
                                ISNULL(@STR_DESCRIPCION_ORIGEN, 'N/D'),
                                '''',
                                ',@INT_ID_TIPO=',
                                ISNULL(@INT_ID_TIPO, -1),
                                ',@STR_DESCRIPCION_TIPO=',
                                '''',
                                ISNULL(@STR_DESCRIPCION_TIPO, 'N/D'),
                                '''',
                                ',@STR_DETALLE=',
                                '''',
                                ISNULL(@STR_DETALLE, 'N/D'),
                                '''',
                                ',@STR_MENSAJE=',
                                '''',
                                ISNULL(@STR_MENSAJE, 'N/D'),
                                '''',
                                ',@STR_MENSAJE_SISTEMA=',
                                '''',
                                ISNULL(@STR_MENSAJE_SISTEMA, 'N/D'),
                                '''',
                                ',@STR_OBSERVACION=',
                                '''',
                                ISNULL(@STR_OBSERVACION, 'N/D'),
                                '''',
                                ',@STR_OTROS_DATOS=',
                                '''',
                                ISNULL(@STR_OTROS_DATOS, 'N/D'),
                                '''',
                                ',@SIN_ID_TIPO_DATOS=',
                                ISNULL(@SIN_ID_TIPO_DATOS, -1),
                                ',@VBI_DATOS=',
                                '''',
                                ISNULL(CONVERT(VARCHAR(MAX), @VBI_DATOS), 'N/D'),
                                '''',
                                ',@STR_SENTENCIA=''',
                                ISNULL(@STR_SENTENCIA, 'N/D'),
                                '''',
                                ',@INT_NUMERO_USUARIO=',
                                ISNULL(@INT_NUMERO_USUARIO, -1),
                                ',@INT_NUMERO_SISTEMA=',
                                ISNULL(@INT_NUMERO_SISTEMA, -1),
                                ',@INT_NIVEL_ANIDAMIENTO=',
                                ISNULL(@INT_NIVEL_ANIDAMIENTO, -1),
                                ',@INT_NUMERO_LINEA=',
                                ISNULL(@INT_NUMERO_LINEA, -1),
                                ',@STR_IDENTIFICADOR_EXTERNO=',
                                '''',
                                ISNULL(@STR_IDENTIFICADOR_EXTERNO, 'N/D'),
                                '''',
                                ',@STR_IDENTIFICADOR_UNICO=',
                                '''',
                                ISNULL(@STR_IDENTIFICADOR_UNICO, 'N/D'),
                                ''''
                            );


                SELECT @STR_ERROR_SENTENCIA = CONCAT('EXEC ', @STR_ERROR_PROCESO, ' ', @STR_ERROR_PARAMETROS);

            END; --FIN-VARIOS                 
        END; --FINAL-DECLARACION Y ASIGNACION DE VARIABLES 
        --++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++   
        -----------------------------                                     
        --CUERPO PROCEDIMIENTO                                     
        -----------------------------        
        BEGIN --INICIO-CUERPO PROCEDIMIENTO 

            IF (@SIN_MODO_EJECUCION = @SIN_MODO_EJECUCION_DEPURACION)
            BEGIN

                SELECT BANDERA_EJECUCION = CONCAT('00000-', @STR_ERROR_PROCESO),
                       FECHA = GETDATE(),
                       SIN_MODO_EJECUCION = @SIN_MODO_EJECUCION,
                       SIN_FK_UTL_PAR_PAIS = @SIN_FK_UTL_PAR_PAIS,
                       STR_USUARIO_CREACION = @STR_USUARIO_CREACION,
                       FEC_CREACION = @FEC_CREACION,
                       STR_USUARIO_MODIFICACION = @STR_USUARIO_MODIFICACION,
                       FEC_MODIFICACION = @FEC_MODIFICACION,
                       INT_ID_SPID = @INT_ID_SPID,
                       FEC_SISTEMA = @FEC_SISTEMA,
                       INT_PRIORIDAD = @INT_PRIORIDAD,
                       INT_SEVERIDAD = @INT_SEVERIDAD,
                       INT_ERROR_ESTADO = @INT_ERROR_ESTADO,
                       STR_SERVIDOR = @STR_SERVIDOR,
                       STR_ESTACION = @STR_ESTACION,
                       STR_BASE_DATOS = @STR_BASE_DATOS,
                       STR_APLICATIVO = @STR_APLICATIVO,
                       STR_CLASE = @STR_CLASE,
                       STR_EVENTO = @STR_EVENTO,
                       STR_PROCESO = @STR_PROCESO,
                       STR_PARAMETROS = @STR_PARAMETROS,
                       INT_ID_ORIGEN = @INT_ID_ORIGEN,
                       STR_DESCRIPCION_ORIGEN = @STR_DESCRIPCION_ORIGEN,
                       INT_ID_TIPO = @INT_ID_TIPO,
                       STR_DESCRIPCION_TIPO = @STR_DESCRIPCION_TIPO,
                       STR_DETALLE = @STR_DETALLE,
                       STR_MENSAJE = @STR_MENSAJE,
                       STR_MENSAJE_SISTEMA = @STR_MENSAJE_SISTEMA,
                       STR_OBSERVACION = @STR_OBSERVACION,
                       STR_OTROS_DATOS = @STR_OTROS_DATOS,
                       SIN_ID_TIPO_DATOS = @SIN_ID_TIPO_DATOS,
                       VBI_DATOS = @VBI_DATOS,
                       STR_DESCRIPCION_TIPO_DATOS = @STR_DESCRIPCION_TIPO_DATOS,
                       STR_SENTENCIA = @STR_SENTENCIA,
                       INT_NUMERO_USUARIO = @INT_NUMERO_USUARIO,
                       INT_NUMERO_SISTEMA = @INT_NUMERO_SISTEMA,
                       INT_NIVEL_ANIDAMIENTO = @INT_NIVEL_ANIDAMIENTO,
                       INT_NUMERO_LINEA = @INT_NUMERO_LINEA,
                       STR_IDENTIFICADOR_EXTERNO = @STR_IDENTIFICADOR_EXTERNO,
                       UNQ_IDENTIFICADOR_UNICO = @UNQ_IDENTIFICADOR_UNICO;

                SELECT BANDERA_EJECUCION = CONCAT('00010-', @STR_ERROR_PROCESO),
                       FECHA = GETDATE(),
                       SENTENCIA_EJECUCION = @STR_ERROR_SENTENCIA;
            END;

            IF (@INT_ID_ORIGEN = @INT_ID_ORIGEN_PROCEDIMIENTO)
            BEGIN

                SELECT TOP 1
                    @STR_APLICATIVO
                        = CONCAT(
                                    'Host:',
                                    ISNULL(vs.host_name, 'N/D'),
                                    ' | Program:',
                                    ISNULL(vs.program_name, 'N,/D'),
                                    ' | Login:',
                                    ISNULL(vs.login_name, 'N,/D')
                                )
                FROM sys.dm_exec_requests vr WITH (NOLOCK)
                    INNER JOIN sys.dm_exec_sessions vs WITH (NOLOCK)
                        ON vr.session_id = vs.session_id
                WHERE vr.session_id = @INT_ID_SPID;
            END;
            -------------------------------------------------------------------------------------------------------------------------------- 
            --EXTRACION DE PARAMETRIZACION GLOBAL, SOBREESCRIBE CUALQUIE CONFIGURACION DEL DETALLE 
            BEGIN

                SELECT @INT_ERROR_NUMERO_USUARIO = 75001;
                --Verbos a utilizar de acuerdo a la operacion=select:seleccionar, delete: eliminar, update: actualizar, insert: insertar, exec: ejecutar 
                SELECT @STR_ERROR_MENSAJE = 'Error, al extraer datos en la tabla <UTL_PAR_GENERALES>';

                SELECT @STR_LLAVE_01_UTL_PAR_GENERALES
                    = 'PA_MAN_TBL_TRA_EXCEPCION_GUARDAR_BIN_REGISTRA_XML_PROCESOS_SISTEMA_GLOBAL';
                SELECT @STR_LLAVE_02_UTL_PAR_GENERALES = '';
                SELECT @STR_LLAVE_03_UTL_PAR_GENERALES = '';

                --INICIO,TICKET=211377,NUM.MODIFICACION=1.0.4,ICORTES,16/02/2017,SE AGREGA MODIFICACION PARA EL FLUJO DEL SALVADOR MANEJO DE TRAZLATE  
                SELECT @SIN_FK_UTL_PAR_PAIS = 0;
                --FROM UTL_PAR_PAIS WITH ( NOLOCK )  WHERE STR_CODIGO=@SIN_FK_UTL_PAR_PAIS OR SIN_PK_UTL_PAR_PAIS=@SIN_FK_UTL_PAR_PAIS  
                --FIN,TICKET=211377,NUM.MODIFICACION=1.0.4,ICORTES,16/02/2017,SE AGREGA MODIFICACION PARA EL FLUJO DEL SALVADOR MANEJO DE TRAZLATE  

                --SELECT  @BIN_REGISTRA_XML_PROCESOS_SISTEMA_GLOBAL = BIN_VALOR_02 
                --FROM    dbo.UTL_PAR_GENERALES WITH ( NOLOCK ) 
                --WHERE   SIN_FK_UTL_PAR_PAIS = @SIN_FK_UTL_PAR_PAIS 
                --        AND STR_LLAVE_01 = @STR_LLAVE_01_UTL_PAR_GENERALES 
                --        AND STR_LLAVE_02 = @STR_LLAVE_02_UTL_PAR_GENERALES 
                --        AND STR_LLAVE_03 = @STR_LLAVE_03_UTL_PAR_GENERALES 

                SELECT @INT_CANTIDAD_REGISTROS = @@ROWCOUNT;

                IF (@INT_CANTIDAD_REGISTROS = 0)
                BEGIN

                    SELECT @INT_ERROR_NUMERO_USUARIO = 75002;
                    --Verbos a utilizar de acuerdo a la operacion=select:seleccionar, delete: eliminar, update: actualizar, insert: insertar, exec: ejecutar 
                    SELECT @STR_ERROR_MENSAJE
                        = CONCAT(
                                    '[Error, no se encuentra datos en tabla <UTL_PAR_GENERALES>, Llave_01:',
                                    @STR_LLAVE_01_UTL_PAR_GENERALES,
                                    ']'
                                );

                    --Levanta el error 
                    RAISERROR(@STR_ERROR_MENSAJE, 16, 1);
                END;
            END;
            -------------------------------------------------------------------------------------------------------------------------------- 
            --EXTRACION DE PARAMETRIZACION GLOBAL, POR APLICATIVO O BASE DE DATOS, SOBREESCRIBE CUALQUIE CONFIGURACION DEL DETALLE 
            BEGIN

                SELECT @INT_ERROR_NUMERO_USUARIO = 75003;
                --Verbos a utilizar de acuerdo a la operacion=select:seleccionar, delete: eliminar, update: actualizar, insert: insertar, exec: ejecutar 
                SELECT @STR_ERROR_MENSAJE = 'Error, al extraer datos en la tabla <UTL_PAR_GENERALES>';

                SELECT @STR_LLAVE_01_UTL_PAR_GENERALES
                    = 'PA_MAN_TBL_TRA_EXCEPCION_GUARDAR_BIN_REGISTRA_XML_PROCESOS_SISTEMA_GLOBAL_MODULO';

                SELECT @STR_LLAVE_02_UTL_PAR_GENERALES
                    = CASE
                          WHEN
                          (
                              @STR_BASE_DATOS != ''
                              AND @STR_BASE_DATOS != 'N/A'
                              AND @INT_ID_ORIGEN IN ( @INT_ID_ORIGEN_PROCEDIMIENTO )
                          ) THEN
                              @STR_BASE_DATOS
                          WHEN
                          (
                              @STR_APLICATIVO != ''
                              AND @STR_APLICATIVO != 'N/A'
                              AND @INT_ID_ORIGEN IN ( @INT_ID_ORIGEN_WINDOWS_FORM, @INT_ID_ORIGEN_WEB_SERVICE,
                                                      @INT_ID_ORIGEN_WEB_SITE, @INT_ID_ORIGEN_REPORTING_SERVICE,
                                                      @INT_ID_ORIGEN_ETL
                                                    )
                          ) THEN
                              @STR_APLICATIVO
                          ELSE
                              'N/D'
                      END;
                SELECT @STR_LLAVE_03_UTL_PAR_GENERALES = '';

                --SELECT  @BIN_REGISTRA_XML_PROCESOS_SISTEMA_GLOBAL_MODULO = BIN_VALOR_02 
                --FROM    dbo.UTL_PAR_GENERALES WITH ( NOLOCK ) 
                --WHERE   SIN_FK_UTL_PAR_PAIS = @SIN_FK_UTL_PAR_PAIS 
                --        AND STR_LLAVE_01 = @STR_LLAVE_01_UTL_PAR_GENERALES 
                --        AND STR_LLAVE_02 = @STR_LLAVE_02_UTL_PAR_GENERALES 
                --        AND STR_LLAVE_03 = @STR_LLAVE_03_UTL_PAR_GENERALES 

                SELECT @INT_CANTIDAD_REGISTROS = @@ROWCOUNT;

                IF (@INT_CANTIDAD_REGISTROS = 0)
                BEGIN

                    SELECT @BIN_REGISTRA_XML_PROCESOS_SISTEMA_GLOBAL_MODULO = -1;

                ----                        SELECT  @INT_ERROR_NUMERO_USUARIO = 75004 
                ----Verbos a utilizar de acuerdo a la operacion=select:seleccionar, delete: eliminar, update: actualizar, insert: insertar, exec: ejecutar 
                ----                        SELECT  @STR_ERROR_MENSAJE = CONCAT('[Error, no se encuentra datos en tabla <UTL_PAR_GENERALES>, Llave_01:', 
                ----                                                          @STR_LLAVE_01_UTL_PAR_GENERALES, 
                ----                                                          ', Llave_02:', 
                ----                                                          @STR_LLAVE_02_UTL_PAR_GENERALES, 
                ----                                                          ']') 

                ----Levanta el error 
                ----                        RAISERROR(@STR_ERROR_MENSAJE,16,1) 
                END;
            END;

            IF (@BIN_REGISTRA_XML_PROCESOS_SISTEMA_GLOBAL_MODULO IN ( 0, 1 ))
            BEGIN
                SELECT @BIN_REGISTRA_XML_PROCESOS_SISTEMA = @BIN_REGISTRA_XML_PROCESOS_SISTEMA_GLOBAL_MODULO;
            END;

            IF (@BIN_REGISTRA_XML_PROCESOS_SISTEMA_GLOBAL IN ( 0, 1 ))
            BEGIN
                SELECT @BIN_REGISTRA_XML_PROCESOS_SISTEMA = @BIN_REGISTRA_XML_PROCESOS_SISTEMA_GLOBAL;
            END;

            IF (@SIN_MODO_EJECUCION = @SIN_MODO_EJECUCION_DEPURACION)
            BEGIN
                SELECT BANDERA_EJECUCION = CONCAT('00020-', @STR_ERROR_PROCESO),
                       FECHA = GETDATE(),
                       BIT_REGISTRA_XML_PROCESOS_SISTEMA = @BIN_REGISTRA_XML_PROCESOS_SISTEMA,
                       BIN_REGISTRA_XML_PROCESOS_SISTEMA_GLOBAL_MODULO = @BIN_REGISTRA_XML_PROCESOS_SISTEMA_GLOBAL_MODULO,
                       BIN_REGISTRA_XML_PROCESOS_SISTEMA_GLOBAL = @BIN_REGISTRA_XML_PROCESOS_SISTEMA_GLOBAL;

            END;

            SELECT @FEC_CREACION = GETDATE();

            --SELECT  @VBI_DATOS 
            SELECT @INT_ERROR_NUMERO_USUARIO = 75005;
            SELECT @STR_ERROR_MENSAJE = 'ERROR AL INSERTAR EN TABLA <TBL_TRA_EXCEPCION>';

            DECLARE @V_EJECUCION NVARCHAR(MAX) = '';


            SELECT @V_EJECUCION
                = N'INSERT  INTO ' + @STR_COMPANNIA
                  + '.TBL_TRA_EXCEPCION 
                        ( SIN_FK_UTL_PAR_PAIS , 
                          STR_USUARIO_CREACION , 
                          FEC_CREACION , 
                          STR_USUARIO_MODIFICACION , 
                          FEC_MODIFICACION , 
                          INT_ID_SPID , 
                          FEC_SISTEMA , 
                          INT_PRIORIDAD , 
                          INT_SEVERIDAD , 
                          INT_ERROR_ESTADO , 
                          STR_SERVIDOR , 
                          STR_ESTACION , 
                          STR_BASE_DATOS , 
                          STR_APLICATIVO , 
                          STR_CLASE , 
                          STR_EVENTO , 
                          STR_PROCESO , 
						  INT_ID_ORIGEN,
                          STR_PARAMETROS ,  
                          STR_DESCRIPCION_ORIGEN , 
                          INT_ID_TIPO , 
                          STR_DESCRIPCION_TIPO , 
                          STR_DETALLE , 
                          STR_MENSAJE , 
                          STR_MENSAJE_SISTEMA , 
                          STR_OBSERVACION , 
                          STR_OTROS_DATOS , 
                          STR_SENTENCIA , 
                          INT_NUMERO_USUARIO , 
                          INT_NUMERO_SISTEMA , 
                          INT_NIVEL_ANIDAMIENTO , 
                          INT_NUMERO_LINEA , 
                          STR_IDENTIFICADOR_EXTERNO , 
                          UNQ_IDENTIFICADOR_UNICO 
                        )'
						
						SELECT @V_EJECUCION +=
						'SELECT  SIN_FK_UTL_PAR_PAIS = @SIN_FK_UTL_PAR_PAIS , 
                                STR_USUARIO_CREACION = @STR_USUARIO_CREACION , 
                                FEC_CREACION = @FEC_CREACION , 
                                STR_USUARIO_MODIFICACION = @STR_USUARIO_MODIFICACION , 
                                FEC_MODIFICACION = @FEC_MODIFICACION , 
                                INT_ID_SPID = @INT_ID_SPID , 
                                FEC_SISTEMA = @FEC_SISTEMA , 
                                INT_PRIORIDAD = @INT_PRIORIDAD , 
                                INT_SEVERIDAD = @INT_SEVERIDAD , 
                                INT_ERROR_ESTADO = @INT_ERROR_ESTADO , 
                                STR_SERVIDOR = @STR_SERVIDOR , 
                                STR_ESTACION = @STR_ESTACION , 
                                STR_BASE_DATOS = @STR_BASE_DATOS , 
                                STR_APLICATIVO = @STR_APLICATIVO , 
                                STR_CLASE = @STR_CLASE , 
                                STR_EVENTO = @STR_EVENTO , 
                                STR_PROCESO = @STR_PROCESO , 
								INT_ID_ORIGEN= @INT_ID_ORIGEN,
                                STR_PARAMETROS = @STR_PARAMETROS , 
                                STR_DESCRIPCION_ORIGEN = @STR_DESCRIPCION_ORIGEN , 
                                INT_ID_TIPO = @INT_ID_TIPO , 
                                STR_DESCRIPCION_TIPO = @STR_DESCRIPCION_TIPO , 
                                STR_DETALLE = @STR_DETALLE , 
                                STR_MENSAJE = @STR_MENSAJE , 
                                STR_MENSAJE_SISTEMA = @STR_MENSAJE_SISTEMA , 
                                STR_OBSERVACION = @STR_OBSERVACION , 
                                STR_OTROS_DATOS = @STR_OTROS_DATOS , 
                                STR_SENTENCIA = @STR_SENTENCIA , 
                                INT_NUMERO_USUARIO = @INT_NUMERO_USUARIO , 
                                INT_NUMERO_SISTEMA = @INT_NUMERO_SISTEMA , 
                                INT_NIVEL_ANIDAMIENTO = @INT_NIVEL_ANIDAMIENTO , 
                                INT_NUMERO_LINEA = @INT_NUMERO_LINEA , 
                                STR_IDENTIFICADOR_EXTERNO = @STR_IDENTIFICADOR_EXTERNO, 
                                UNQ_IDENTIFICADOR_UNICO = @UNQ_IDENTIFICADOR_UNICO;';


            EXEC sys.sp_executesql @V_EJECUCION,
                                   N'@SIN_FK_UTL_PAR_PAIS INT, 
    @STR_USUARIO_CREACION VARCHAR(50) , 
    @FEC_CREACION DATETIME, 
    @STR_USUARIO_MODIFICACION VARCHAR(50) , 
    @FEC_MODIFICACION DATETIME  , 
    @INT_ID_SPID INT , 
    @FEC_SISTEMA DATETIME , 
    @INT_PRIORIDAD INT, 
    @INT_SEVERIDAD INT  , 
    @INT_ERROR_ESTADO INT , 
    @STR_SERVIDOR VARCHAR(300), 
    @STR_ESTACION VARCHAR(300)  , 
    @STR_BASE_DATOS VARCHAR(300)  , 
    @STR_APLICATIVO VARCHAR(300) , 
    @STR_CLASE VARCHAR(300)  , 
    @STR_EVENTO VARCHAR(300) , 
    @STR_PROCESO VARCHAR(300)  , 
    @STR_PARAMETROS VARCHAR(MAX) , 
    @INT_ID_ORIGEN INT ,
    @STR_DESCRIPCION_ORIGEN VARCHAR(300) ,
    @INT_ID_TIPO INT , 
    @STR_DESCRIPCION_TIPO VARCHAR(300) , 
    @STR_DETALLE VARCHAR(500) , 
    @STR_MENSAJE VARCHAR(MAX)  , 
    @STR_MENSAJE_SISTEMA VARCHAR(MAX)  , 
    @STR_OBSERVACION VARCHAR(MAX)  , 
    @STR_OTROS_DATOS VARCHAR(MAX) , 
    @STR_SENTENCIA VARCHAR(MAX), 
    @INT_NUMERO_USUARIO INT  , 
    @INT_NUMERO_SISTEMA INT  , 
    @INT_NIVEL_ANIDAMIENTO INT  , 
    @INT_NUMERO_LINEA INT , 
    @STR_IDENTIFICADOR_EXTERNO VARCHAR(50), 
	@UNQ_IDENTIFICADOR_UNICO VARCHAR(50)',
                                   @SIN_FK_UTL_PAR_PAIS = @SIN_FK_UTL_PAR_PAIS,
                                   @STR_USUARIO_CREACION = @STR_USUARIO_CREACION,
                                   @FEC_CREACION = @FEC_CREACION,
                                   @STR_USUARIO_MODIFICACION = @STR_USUARIO_MODIFICACION,
                                   @FEC_MODIFICACION = @FEC_MODIFICACION,
                                   @INT_ID_SPID = @INT_ID_SPID,
                                   @FEC_SISTEMA = @FEC_SISTEMA,
                                   @INT_PRIORIDAD = @INT_PRIORIDAD,
                                   @INT_SEVERIDAD = @INT_SEVERIDAD,
                                   @INT_ERROR_ESTADO = @INT_ERROR_ESTADO,
                                   @STR_SERVIDOR = @STR_SERVIDOR,
                                   @STR_ESTACION = @STR_ESTACION,
                                   @STR_BASE_DATOS = @STR_BASE_DATOS,
                                   @STR_APLICATIVO = @STR_APLICATIVO,
                                   @STR_CLASE = @STR_CLASE,
                                   @STR_EVENTO = @STR_EVENTO,
                                   @STR_PROCESO = @STR_PROCESO,
                                   @STR_PARAMETROS = @STR_PARAMETROS,
                                   @INT_ID_ORIGEN = @INT_ID_ORIGEN,
                                   @STR_DESCRIPCION_ORIGEN = @STR_DESCRIPCION_ORIGEN,
                                   @INT_ID_TIPO = @INT_ID_TIPO,
                                   @STR_DESCRIPCION_TIPO = @STR_DESCRIPCION_TIPO,
                                   @STR_DETALLE = @STR_DETALLE,
                                   @STR_MENSAJE = @STR_MENSAJE,
                                   @STR_MENSAJE_SISTEMA = @STR_MENSAJE_SISTEMA,
                                   @STR_OBSERVACION = @STR_OBSERVACION,
                                   @STR_OTROS_DATOS = @STR_OTROS_DATOS,
                                   @STR_SENTENCIA = @STR_SENTENCIA,
                                   @INT_NUMERO_USUARIO = @INT_NUMERO_USUARIO,
                                   @INT_NUMERO_SISTEMA = @INT_NUMERO_SISTEMA,
                                   @INT_NIVEL_ANIDAMIENTO = @INT_NIVEL_ANIDAMIENTO,
                                   @INT_NUMERO_LINEA = @INT_NUMERO_LINEA,
                                   @STR_IDENTIFICADOR_EXTERNO = @STR_IDENTIFICADOR_EXTERNO,
                                   @UNQ_IDENTIFICADOR_UNICO = @UNQ_IDENTIFICADOR_UNICO;


            SELECT @BIN_PK_TBL_TRA_EXCEPCION = SCOPE_IDENTITY();


            IF (@VBI_DATOS != NULL)
            BEGIN
                SELECT @FEC_CREACION = GETDATE();

                SELECT @INT_ERROR_NUMERO_USUARIO = 75006;
                SELECT @STR_ERROR_MENSAJE = 'ERROR AL INSERTAR EN TABLA <TBL_TRA_EXCEPCION_BINARIOS>';

            -- INSERT  INTO TBL_TRA_EXCEPCION_BINARIOS 
            --         ( SIN_FK_UTL_PAR_PAIS , 
            --           STR_USUARIO_CREACION , 
            --           FEC_CREACION , 
            --           STR_USUARIO_MODIFICACION , 
            --           FEC_MODIFICACION , 
            --           SIN_ID_TIPO_DATOS , 
            --           VBI_DATOS , 
            --           STR_DESCRIPCION_TIPO_DATOS , 
            --           UNQ_IDENTIFICADOR_UNICO , 
            --           BIN_FK_TBL_TRA_EXCEPCION 
            --) 
            --         SELECT  SIN_FK_UTL_PAR_PAIS = @SIN_FK_UTL_PAR_PAIS , 
            --                 STR_USUARIO_CREACION = @STR_USUARIO_CREACION , 
            --                 FEC_CREACION = @FEC_CREACION , 
            --                 STR_USUARIO_MODIFICACION = '' , 
            --                 FEC_MODIFICACION = '19000101' , 
            --                 SIN_ID_TIPO_DATOS = @SIN_ID_TIPO_DATOS , 
            --                 VBI_DATOS = @VBI_DATOS , 
            --                 STR_DESCRIPCION_TIPO_DATOS = @STR_DESCRIPCION_TIPO_DATOS , 
            --                 UNQ_IDENTIFICADOR_UNICO = @UNQ_IDENTIFICADOR_UNICO , 
            --                 BIN_FK_TBL_TRA_EXCEPCION = @BIN_PK_TBL_TRA_EXCEPCION 
            END;

            IF (@SIN_MODO_EJECUCION = @SIN_MODO_EJECUCION_DEPURACION)
            BEGIN
                SELECT BANDERA_EJECUCION = CONCAT('00040-', @STR_ERROR_PROCESO),
                       FECHA = GETDATE(),
                       BIT_REGISTRA_XML_PROCESOS_SISTEMA = @BIN_REGISTRA_XML_PROCESOS_SISTEMA,
                       INT_ID_ORIGEN = @INT_ID_ORIGEN,
                       INT_ID_ORIGEN_PROCEDIMIENTO = @INT_ID_ORIGEN_PROCEDIMIENTO,
                       UNQ_IDENTIFICADOR_UNICO = @UNQ_IDENTIFICADOR_UNICO;

                --SELECT TOP 20 
                --        BANDERA_EJECUCION = CONCAT('00045-', 
                --                                   @STR_ERROR_PROCESO) , 
                --        FECHA = GETDATE() , 
                --        * 
                --FROM    TBL_TRA_EXCEPCION_BINARIOS 
                --ORDER BY BIN_PK_TBL_TRA_EXCEPCION_BINARIOS DESC 


                SELECT SIN_FK_UTL_PAR_PAIS = @SIN_FK_UTL_PAR_PAIS,
                       STR_USUARIO_CREACION = @STR_USUARIO_CREACION,
                       FEC_CREACION = @FEC_CREACION,
                       STR_USUARIO_MODIFICACION = '',
                       FEC_MODIFICACION = '19000101',
                       SIN_ID_TIPO_DATOS = @SIN_ID_TIPO_DATOS,
                       VBI_DATOS = @VBI_DATOS,
                       STR_DESCRIPCION_TIPO_DATOS = @STR_DESCRIPCION_TIPO_DATOS,
                       UNQ_IDENTIFICADOR_UNICO = @UNQ_IDENTIFICADOR_UNICO,
                       BIN_FK_TBL_TRA_EXCEPCION = @BIN_PK_TBL_TRA_EXCEPCION;

            END;

            --00000-00 FIX,INICIO,JALFARO,SE COMENTA NO SE UTILIZA Y GENERA ESPACIO 
            --IF ( @BIN_REGISTRA_XML_PROCESOS_SISTEMA = 1 ) 
            --    AND ( @INT_ID_ORIGEN IN ( @INT_ID_ORIGEN_PROCEDIMIENTO ) )  
            --    BEGIN 
            --        SELECT  @FEC_CREACION = GETDATE() 

            --        SELECT  @INT_ERROR_NUMERO_USUARIO = 75007 
            --        SELECT  @STR_ERROR_MENSAJE = 'ERROR AL EXTRAER DATOS EN VARIABLE <@VBI_DATOS>' 

            --        SELECT  @XML_DATOS = ( SELECT   Session_id = vr.session_id , 
            --                                        Blocking_session_id = vr.blocking_session_id , 
            --                                        Start_Time = vr.start_time , 
            --                                        Duration_Time = DATEDIFF(mi, 
            --                                              vr.start_time, 
            --                                              GETDATE()) , 
            --                                        Command = vr.command , 
            --                                        DatabaseName = db.name , 
            --                                        ObjectName = ISNULL(OBJECT_NAME(sqlt.objectid, 
            --                                              dbid), '') , 
            --                                        SQL_Text = sqlt.text , 
            --                                        Status = vr.status , 
            --                                        Login_Name = vs.login_name , 
            --                                        Host_Name = vs.host_name , 
            --                                        Program_Name = vs.program_name , 
            --                                        Time_Blocking = vr.wait_time 
            --                                        / 1000 , 
            --                                        CPU_Time = vr.cpu_time , 
            --                                        Reads = vr.logical_reads , 
            --                                        SQL_handle = sql_handle , 
            --                                        Plan_handle = plan_handle 
            --                               FROM     sys.dm_exec_requests vr 
            --                                        INNER JOIN sys.dm_exec_sessions vs ON vr.session_id = vs.session_id 
            --                                        CROSS APPLY sys.dm_exec_sql_text(sql_handle) sqlt 
            --                                        INNER JOIN sys.databases db ON db.database_id = vr.database_id 
            --                             FOR 
            --                               XML RAW('exec_requests') , 
            --                                   ROOT('process_data') , 
            --                                   ELEMENTS XSINIL , 
            --                                   BINARY BASE64 
            --                             ) 

            --        --SELECT  @VBI_DATOS = CONVERT(VARBINARY(MAX), @XML_DATOS) 

            --        SELECT  @STR_DESCRIPCION_TIPO_DATOS = CONCAT('XML, Informacion sobre procesos en BD, variable @BIT_REGISTRA_XML_PROCESOS_SISTEMA:', 
            --                                              @BIN_REGISTRA_XML_PROCESOS_SISTEMA) 
            --        SELECT  @SIN_ID_TIPO_DATOS = @SIN_ID_TIPO_DATOS_XML 

            --        SELECT  @INT_ERROR_NUMERO_USUARIO = 75008 
            --        SELECT  @STR_ERROR_MENSAJE = 'ERROR AL INSERTAR EN TABLA <TBL_TRA_EXCEPCION_BINARIOS>' 

            --        INSERT  INTO TBL_TRA_EXCEPCION_BINARIOS 
            --                ( SIN_FK_UTL_PAR_PAIS , 
            --                  STR_USUARIO_CREACION , 
            --                  FEC_CREACION , 
            --                  STR_USUARIO_MODIFICACION , 
            --                  FEC_MODIFICACION , 
            --                  SIN_ID_TIPO_DATOS , 
            --                  VBI_DATOS , 
            --                  STR_DESCRIPCION_TIPO_DATOS , 
            --                  UNQ_IDENTIFICADOR_UNICO , 
            --                  BIN_FK_TBL_TRA_EXCEPCION 
            --       ) 
            --                SELECT  SIN_FK_UTL_PAR_PAIS = @SIN_FK_UTL_PAR_PAIS , 
            --                        STR_USUARIO_CREACION = @STR_USUARIO_CREACION , 
            --                        FEC_CREACION = @FEC_CREACION , 
            --                        STR_USUARIO_MODIFICACION = '' , 
            --                        FEC_MODIFICACION = '19000101' , 
            --                        SIN_ID_TIPO_DATOS = @SIN_ID_TIPO_DATOS , 
            --                        VBI_DATOS = @VBI_DATOS , 
            --                        STR_DESCRIPCION_TIPO_DATOS = @STR_DESCRIPCION_TIPO_DATOS , 
            --                        UNQ_IDENTIFICADOR_UNICO = NEWID() , 
            --                        BIN_FK_TBL_TRA_EXCEPCION = @BIN_PK_TBL_TRA_EXCEPCION      

            --    END    
            --00000-00 FIX,FIN,JALFARO,SE COMENTA NO SE UTILIZA Y GENERA ESPACIO     

            IF (@SIN_MODO_EJECUCION = @SIN_MODO_EJECUCION_DEPURACION)
            BEGIN
                SELECT BANDERA_EJECUCION = CONCAT('999999-', @STR_ERROR_PROCESO),
                       FECHA = GETDATE(),
                       XML_DATOS = @XML_DATOS;
            END;
        --++++++++++++++++++++++++++++++++++++++++++++++++++++++++ 
        /*SI SE DESEA REGISTRAR EL ERROR EN UN LUGAR CENTRALIZADO, COLOCARLO DESPUES DEL  
			INGRESO LOCAL, SIN MANEJO TRANSACCIONAL, POR SI SE CAE REGISTRE AL MENOS LOCALMENTE*/
        --++++++++++++++++++++++++++++++++++++++++++++++++++++++++ 
        END; --FIN-CUERPO PROCEDIMIENTO		 

        --COMMIT TRANSACTION 

        RETURN 0;
    --+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++                  
    END TRY
    BEGIN CATCH
        PRINT CONCAT('global try', ERROR_MESSAGE());
        IF (@SIN_MODO_EJECUCION = @SIN_MODO_EJECUCION_DEPURACION)
        BEGIN
            SELECT BANDERA = CONCAT('ERROR-99999-01-', @STR_ERROR_PROCESO),
                   FECHA = GETDATE(),
                   [XACT_STATE] = XACT_STATE(),
                   TRANCOUNT = @@TRANCOUNT;
        END;

        -- VALIDA SI LA TRANSACCION NO SE PUEDE HACER COMMIT 
        --IF ( XACT_STATE() ) = -1  
        --    BEGIN 
        --        IF ( @@TRANCOUNT > 0 )  
        --            BEGIN 
        --                ROLLBACK TRANSACTION 
        ----            END 
        --    END 

        SELECT @STR_ERROR_OBSERVACION
            = '(' + CONVERT(VARCHAR, ISNULL(@@NESTLEVEL, 0)) + ')-PROCESO:' + ISNULL(@STR_ERROR_PROCESO, 'N/D')
              + ',PROCESO ERROR:' + CONVERT(VARCHAR, ISNULL(ERROR_PROCEDURE(), 0)) + ',NUMERO USUARIO:'
              + CONVERT(VARCHAR, ISNULL(@INT_ERROR_NUMERO_USUARIO, 0)) + ',NUMERO SISTEMA:'
              + CONVERT(VARCHAR, ISNULL(ERROR_NUMBER(), 0)) + ',MENSAJE:' + ISNULL(@STR_ERROR_MENSAJE, 'N/D')
              + ',LINEA ERROR:' + CONVERT(VARCHAR, ISNULL(ERROR_LINE(), 0)) + '|' + ISNULL(ERROR_MESSAGE(), 'N/D');

        RAISERROR(@STR_ERROR_OBSERVACION, 16, 1);

        RETURN @INT_ERROR_NUMERO_USUARIO;
    END CATCH;

    RETURN 0;

END;
