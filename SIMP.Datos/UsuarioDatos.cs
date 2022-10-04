using SIMP.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Datos
{
    public class UsuarioDatos
    {
        public List<UsuarioEntidad> GetUsuarios(UsuarioEntidad usuario)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = $"{usuario.Esquema}.PA_CON_USUARIO";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_USUARIO", usuario.Usuario);
                cmd.Parameters.AddWithValue("@P_USUARIO_SISTEMA", usuario.Usuario);
                cmd.Parameters.AddWithValue("@P_OPCION", usuario.Opcion);
                cmd.Parameters.AddWithValue("@P_ID", usuario.Id);
                cmd.Parameters.AddWithValue("@P_USUARIO_SISTEMA", usuario.Usuario_Sistema);
                cmd.Parameters.AddWithValue("@P_IDESTADO", usuario.Estado);
                cmd.Parameters.AddWithValue("@P_ESQUEMA", usuario.Esquema);
                cmd.Parameters.AddWithValue("@P_CONTRASENA", usuario.Contrasena);

                myConexion.Open();
                reader = cmd.ExecuteReader();

                List<UsuarioEntidad> lista = new List<UsuarioEntidad>();

                while (reader.Read())
                {
                    UsuarioEntidad obj = new UsuarioEntidad();
                    obj.Id = UtilitarioSQL.ObtieneInt(reader, "PK_TBL_SIMP_SEG_USUARIO");
                    obj.Usuario_Sistema = UtilitarioSQL.ObtieneString(reader, "USUARIO_SISTEMA");
                    obj.Nombre = UtilitarioSQL.ObtieneString(reader, "NOMBRE");
                    obj.Correo = UtilitarioSQL.ObtieneString(reader, "CORREO");
                    obj.Perfil = UtilitarioSQL.ObtieneInt(reader, "FK_TBL_SIMP_SEG_PERFIL");
                    obj.Contrasena = UtilitarioSQL.ObtieneString(reader, "CONTRASENA");
                    obj.Estado = UtilitarioSQL.ObtieneInt(reader, "FK_TBL_SIMP_ESTADO");
                    obj.Cambio_Clave = UtilitarioSQL.ObtieneString(reader, "CAMBIO_CLAVE");
                    obj.PerfilNombre = UtilitarioSQL.ObtieneString(reader, "FK_TBL_SIMP_SEG_PERFIL_NOMBRE");
                    lista.Add(obj);
                }
                reader.Dispose();
                cmd.Dispose();
                myConexion.Close();
                myConexion.Dispose();
                return lista;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error en Base de Datos" + ex.Message);
            }
        }
        public void MantUsuario(UsuarioEntidad usuario)
        {
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                myConexion = new SqlConnection(Conexion.CadenaDeConexion());
                string Sql = $"{usuario.Esquema}.PA_MAN_USUARIO";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_USUARIO", usuario.Usuario);
                cmd.Parameters.AddWithValue("@P_OPCION", usuario.Opcion);
                cmd.Parameters.AddWithValue("@P_ID", usuario.Id);
                cmd.Parameters.AddWithValue("@P_USUARIO_SISTEMA", usuario.Usuario_Sistema);
                cmd.Parameters.AddWithValue("@P_NOMBRE", usuario.Nombre);
                cmd.Parameters.AddWithValue("@P_CORREO", usuario.Correo);
                cmd.Parameters.AddWithValue("@P_IDPERFIL", usuario.Perfil);
                if (!string.IsNullOrEmpty(usuario.Contrasena))
                {
                    string encriptada = EncriptarString(usuario.Contrasena);
                    cmd.Parameters.AddWithValue("@P_CONTRASENNA", encriptada);
                }
                cmd.Parameters.AddWithValue("@P_IDESTADO", usuario.Estado);
                cmd.Parameters.AddWithValue("@P_CAMBIO_CLAVE", usuario.Cambio_Clave);               
                cmd.Parameters.AddWithValue("@P_ESQUEMA", usuario.Esquema);


                myConexion.Open();
                reader = cmd.ExecuteReader();

                reader.Dispose();
                cmd.Dispose();
                myConexion.Close();
                myConexion.Dispose();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error en Base de Datos" + ex.Message);
            }
        }

        #region Encriptar;

        public string DesencriptarString(string textoEncriptado)
        {
            return Desencriptar(textoEncriptado, "sitsa@2012SOFT", "s@lAvz", "MD5",
              1, "@1B2c3D4e5F6g7H8", 128);
        }

        public string EncriptarString(string stringEncriptado)
        {
            return Encriptar(stringEncriptado,
              "sitsa@2012SOFT", "s@lAvz", "MD5", 1, "@1B2c3D4e5F6g7H8", 128);
        }

        public string Encriptar(string textoQueEncriptaremos, string passBase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(textoQueEncriptaremos);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passBase,
              saltValueBytes, hashAlgorithm, passwordIterations);
            byte[] keyBytes = password.GetBytes(keySize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged()
            {
                Mode = CipherMode.CBC
            };
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes,
              initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor,
             CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string cipherText = Convert.ToBase64String(cipherTextBytes);
            return cipherText;
        }

        public string Desencriptar(string textoEncriptado, string passBase,
        string saltValue, string hashAlgorithm, int passwordIterations,
        string initVector, int keySize)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] cipherTextBytes = Convert.FromBase64String(textoEncriptado);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passBase,
              saltValueBytes, hashAlgorithm, passwordIterations);
            byte[] keyBytes = password.GetBytes(keySize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged()
            {
                Mode = CipherMode.CBC
            };
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes,
              initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor,
              CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0,
              plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            string plainText = Encoding.UTF8.GetString(plainTextBytes, 0,
              decryptedByteCount);
            return plainText;
        }

        #endregion;
    }
}
