﻿using SIMP.Entidades;
using SIMP.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using SIMP.Datos;
using System.Net;
using System.Drawing.Printing;
using System.Collections;
using SIMP.Logica.UTILITIES;

namespace SIMP
{
  public partial class Reporte : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (Session["UsuarioSistema"] == null)
      {
        Response.Redirect("Login.aspx");
      }
      if (!IsPostBack)
      {
        HabilitaOpcionesPermisos();
        CargarGridProgresoProyecto();

      }
    }
    private void CargarGridProgresoProyecto()
    {
      try
      {
        List<ProgresoProyectoEntidad> lstProyectoEntidad = new List<ProgresoProyectoEntidad>();
        lstProyectoEntidad = ProgresoProyectoLogica.GetProgresoProyecto(new ProgresoProyectoEntidad()
        {
          Esquema = "dbo",
          Opcion = 0
        });
        lstProyectoEntidad.ForEach(x =>
        {
          x.NombreEstado = x.Estado == "1" ? "Activo" : "Inactivo";
        });
        gvProgresoProyecto.DataSource = lstProyectoEntidad;
        gvProgresoProyecto.DataBind();
      }
      catch (Exception ex)
      {
        Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
      }
    }
    private void HabilitaOpcionesPermisos()
    {
      try
      {
        string nombreUrl = Request.Url.Segments[Request.Url.Segments.Length - 1].ToString();
        if (Session["Permiso_" + nombreUrl] != null)
        {
          MenuEntidad obMenu = (Session["Permiso_" + nombreUrl] as MenuEntidad);
          string permisos = string.Empty;

          if (!obMenu.CrearPermiso)
          {
            btnBuscarProgresoProyecto.Visible = false;

            permisos += "- Crear ";
          }

          if (!obMenu.EditarPermiso)
          {
            gvProgresoProyecto.Columns[9].Visible = false;
            gvProgresoProyecto.Columns[10].Visible = false;
            gvProgresoProyecto.Columns[11].Visible = false;
            permisos += "- Editar ";
          }

          if (!obMenu.VerPermiso)
          {
            gvProgresoProyecto.Visible = false;

            permisos += "- Consultar ";
          }

          if (obMenu.EnviarPermiso)
          {
            //hdfPermisoEnviarCorreos.Value = "1";
          }
          else
          {
            //hdfPermisoEnviarCorreos.Value = "0";
            permisos += "- Enviar Correos";
          }

          if (!string.IsNullOrEmpty(permisos))
          {
            mensajePermiso.Visible = true;
            lblMensajePermisos.Text = "El usuario no cuenta con permisos para: " + permisos;
          }
        }
      }
      catch (Exception ex)
      {
        Mensaje("Error", ex.Message.Replace("'", "").Replace("\n", "").Replace("\r", ""), false);
      }
    }
    protected void btnBuscarProgresoProyecto_Click(object sender, EventArgs e)
    {

    }

    protected void gvProgresoProyecto_PreRender(object sender, EventArgs e)
    {

    }

    protected void gvProgresoProyecto_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      string error = string.Empty;

      try
      {
        if(e.CommandName == "GenerarPDF")
        {
          Hashtable parametrosReporte = new Hashtable();
          parametrosReporte.Add("@P_USUARIO", "hcalvo");
          parametrosReporte.Add("@P_OPCION", 0);
          parametrosReporte.Add("@P_PK_TBL_SIMP_PY_PROYECTO", "0");
          parametrosReporte.Add("@P_FECHA_INICIO", null);
          parametrosReporte.Add("@P_FECHA_FINAL", null);
          parametrosReporte.Add("@P_ESQUEMA", "DBO");

          string rutaReportes = Server.MapPath(Path.Combine("Reportes", "ReportePorcentajeProyecto.rpt"));




          PrintHelper.imprimirDocumento(parametrosReporte, "PrimoPDF", rutaReportes);
        }




        //Version Ecommerce
        //if (e.CommandName == "GenerarPDF")
        //{
        //  int index = Convert.ToInt32(e.CommandArgument);
        //  string rutaReportes = Server.MapPath(Path.Combine("Reportes", "ReportePorcentajeProyecto.rpt"));
        //  ConnectionInfo conex = new ConnectionInfo();
        //  Conexion con = new Conexion();
        //  conex.DatabaseName = con.ObtenerDato(1);  //"Exactus";
        //  conex.UserID = con.ObtenerDato(2);  //"ECOMMERCE";  
        //  conex.Password = con.ObtenerDato(3); //"Sa123";
        //  conex.ServerName = con.ObtenerDato(0); // "Exactus";
        //  conex.Type = ConnectionInfoType.SQL;

        //  ReportDocument rd = new ReportDocument();
        //  rd.FileName = rutaReportes;
        //  rd.Refresh();
        //  rd.Load(rutaReportes);

        //  rd.SetParameterValue("@P_USUARIO", "hcalvo");
        //  rd.SetParameterValue("@P_OPCION", "0");
        //  rd.SetParameterValue("@P_ESQUEMA", "DBO");
        //  //    crystalReportViewer1.ParameterFieldInfo = pSucursales;




        //  rd.SetDatabaseLogon(con.ObtenerDato(2), con.ObtenerDato(3));


        //  #region Extensiones

        //  String[] extensiones = new String[] { "application/msword.docx", "application/vnd.ms-excel.xlsx", "x-world/x-3dmf.3dm", "x-world/x-3dmf.3dmf", "application/octet-stream.a", "application/x-authorware-bin.aab", "application/x-authorware-map.aam", "application/x-authorware-seg.aas", "text/vnd.abc.abc", "text/html.acgi", "video/animaflex.afl", "application/postscript.ai", "audio/aiff.aif", "audio/x-aiff.aif", "audio/aiff.aifc", "audio/x-aiff.aifc", "audio/aiff.aiff", "audio/x-aiff.aiff", "application/x-aim.aim", "text/x-audiosoft-intra.aip", "application/x-navi-animation.ani", "application/x-nokia-9000-communicator-add-on-software.aos", "application/mime.aps", "application/octet-stream.arc", "application/arj.arj", "application/octet-stream.arj", "image/x-jg.art", "video/x-ms-asf.asf", "text/x-asm.asm", "text/asp.asp", "application/x-mplayer2.asx", "video/x-ms-asf.asx", "video/x-ms-asf-plugin.asx", "audio/basic.au", "audio/x-au.au", "application/x-troff-msvideo.avi", "video/avi.avi", "video/msvideo.avi", "video/x-msvideo.avi", "video/avs-video.avs", "application/x-bcpio.bcpio", "application/mac-binary.bin", "application/macbinary.bin", "application/octet-stream.bin", "application/x-binary.bin", "application/x-macbinary.bin", "image/bmp.bm", "image/bmp.bmp", "image/x-windows-bmp.bmp", "application/book.boo", "application/book.book", "application/x-bzip2.boz", "application/x-bsh.bsh", "application/x-bzip.bz", "application/x-bzip2.bz2", "text/plain.c", "text/x-c.c", "text/plain.c++", "application/vnd.ms-pki.seccat.cat", "text/plain.cc", "text/x-c.cc", "application/clariscad.ccad", "application/x-cocoa.cco", "application/cdf.cdf", "application/x-cdf.cdf", "application/x-netcdf.cdf", "application/pkix-cert.cer", "application/x-x509-ca-cert.cer", "application/x-chat.cha", "application/x-chat.chat", "application/java.class", "application/java-byte-code.class", "application/x-java-class.class", "application/octet-stream.com", "text/plain.com", "text/plain.conf", "application/x-cpio.cpio", "text/x-c.cpp", "application/mac-compactpro.cpt", "application/x-compactpro.cpt", "application/x-cpt.cpt", "application/pkcs-crl.crl", "application/pkix-crl.crl", "application/pkix-cert.crt", "application/x-x509-ca-cert.crt", "application/x-x509-user-cert.crt", "application/x-csh.csh", "text/x-script.csh.csh", "application/x-pointplus.css", "text/css.css", "text/plain.cxx", "application/x-director.dcr", "application/x-deepv.deepv", "text/plain.def", "application/x-x509-ca-cert.der", "video/x-dv.dif", "application/x-director.dir", "video/dl.dl", "video/x-dl.dl", "application/msword.doc", "application/msword.dot", "application/commonground.dp", "application/drafting.drw", "application/octet-stream.dump", "video/x-dv.dv", "application/x-dvi.dvi", "drawing/x-dwf (old).dwf", "model/vnd.dwf.dwf", "application/acad.dwg", "image/vnd.dwg.dwg", "image/x-dwg.dwg", "application/dxf.dxf", "image/vnd.dwg.dxf", "image/x-dwg.dxf", "application/x-director.dxr", "text/x-script.elisp.el", "application/x-bytecode.elisp (compiled elisp).elc", "application/x-elc.elc", "application/x-envoy.env", "application/postscript.eps", "application/x-esrehber.es", "text/x-setext.etx", "application/envoy.evy", "application/x-envoy.evy", "application/octet-stream.exe", "text/plain.f", "text/x-fortran.f", "text/x-fortran.f77", "text/plain.f90", "text/x-fortran.f90", "application/vnd.fdf.fdf", "application/fractals.fif", "image/fif.fif", "video/fli.fli", "video/x-fli.fli", "image/florian.flo", "text/vnd.fmi.flexstor.flx", "video/x-atomic3d-feature.fmf", "text/plain.for", "text/x-fortran.for", "image/vnd.fpx.fpx", "image/vnd.net-fpx.fpx", "application/freeloader.frl", "audio/make.funk", "text/plain.g", "image/g3fax.g3", "image/gif.gif", "video/gl.gl", "video/x-gl.gl", "audio/x-gsm.gsd", "audio/x-gsm.gsm", "application/x-gsp.gsp", "application/x-gss.gss", "application/x-gtar.gtar", "application/x-compressed.gz", "application/x-gzip.gz", "application/x-gzip.gzip", "multipart/x-gzip.gzip", "text/plain.h", "text/x-h.h", "application/x-hdf.hdf", "application/x-helpfile.help", "application/vnd.hp-hpgl.hgl", "text/plain.hh", "text/x-h.hh", "text/x-script.hlb", "application/hlp.hlp", "application/x-helpfile.hlp", "application/x-winhelp.hlp", "application/vnd.hp-hpgl.hpg", "application/vnd.hp-hpgl.hpgl", "application/binhex.hqx", "application/binhex4.hqx", "application/mac-binhex.hqx", "application/mac-binhex40.hqx", "application/x-binhex40.hqx", "application/x-mac-binhex40.hqx", "application/hta.hta", "text/x-component.htc", "text/html.htm", "text/html.html", "text/html.htmls", "text/webviewhtml.htt", "text/html.htx", "x-conference/x-cooltalk.ice", "image/x-icon.ico", "text/plain.idc", "image/ief.ief", "image/ief.iefs", "application/iges.iges", "model/iges.iges", "application/iges.igs", "model/iges.igs", "application/x-ima.ima", "application/x-httpd-imap.imap", "application/inf.inf", "application/x-internett-signup.ins", "application/x-ip2.ip", "video/x-isvideo.isu", "audio/it.it", "application/x-inventor.iv", "i-world/i-vrml.ivr", "application/x-livescreen.ivy", "audio/x-jam.jam", "text/plain.jav", "text/x-java-source.jav", "text/plain.java", "text/x-java-source.java", "application/x-java-commerce.jcm", "image/jpeg.jfif", "image/pjpeg.jfif", "image/jpeg.jfif-tbnl", "image/jpeg.jpe", "image/pjpeg.jpe", "image/jpeg.jpeg", "image/pjpeg.jpeg", "image/jpeg.jpg", "image/pjpeg.jpg", "image/x-jps.jps", "application/x-javascript.js", "image/jutvision.jut", "audio/midi.kar", "music/x-karaoke.kar", "application/x-ksh.ksh", "text/x-script.ksh.ksh", "audio/nspaudio.la", "audio/x-nspaudio.la", "audio/x-liveaudio.lam", "application/x-latex.latex", "application/lha.lha", "application/octet-stream.lha", "application/x-lha.lha", "application/octet-stream.lhx", "text/plain.list", "audio/nspaudio.lma", "audio/x-nspaudio.lma", "text/plain.log", "application/x-lisp.lsp", "text/x-script.lisp.lsp", "text/plain.lst", "text/x-la-asf.lsx", "application/x-latex.ltx", "application/octet-stream.lzh", "application/x-lzh.lzh", "application/lzx.lzx", "application/octet-stream.lzx", "application/x-lzx.lzx", "text/plain.m", "text/x-m.m", "video/mpeg.m1v", "audio/mpeg.m2a", "video/mpeg.m2v", "audio/x-mpequrl.m3u", "application/x-troff-man.man", "application/x-navimap.map", "text/plain.mar", "application/mbedlet.mbd", "application/x-magic-cap-package-1.0.mc$", "application/mcad.mcd", "application/x-mathcad.mcd", "image/vasa.mcf", "text/mcf.mcf", "application/netmc.mcp", "application/x-troff-me.me", "message/rfc822.mht", "message/rfc822.mhtml", "application/x-midi.mid", "audio/midi.mid", "audio/x-mid.mid", "audio/x-midi.mid", "music/crescendo.mid", "x-music/x-midi.mid", "application/x-midi.midi", "audio/midi.midi", "audio/x-mid.midi", "audio/x-midi.midi", "music/crescendo.midi", "x-music/x-midi.midi", "application/x-frame.mif", "application/x-mif.mif", "message/rfc822.mime", "www/mime.mime", "audio/x-vnd.audioexplosion.mjuicemediafile.mjf", "video/x-motion-jpeg.mjpg", "application/base64.mm", "application/x-meme.mm", "application/base64.mme", "audio/mod.mod", "audio/x-mod.mod", "video/quicktime.moov", "video/quicktime.mov", "video/x-sgi-movie.movie", "audio/mpeg.mp2", "audio/x-mpeg.mp2", "video/mpeg.mp2", "video/x-mpeg.mp2", "video/x-mpeq2a.mp2", "audio/mpeg3.mp3", "audio/x-mpeg-3.mp3", "video/mpeg.mp3", "video/x-mpeg.mp3", "audio/mpeg.mpa", "video/mpeg.mpa", "application/x-project.mpc", "video/mpeg.mpe", "video/mpeg.mpeg", "audio/mpeg.mpg", "video/mpeg.mpg", "audio/mpeg.mpga", "application/vnd.ms-project.mpp", "application/x-project.mpt", "application/x-project.mpv", "application/x-project.mpx", "application/marc.mrc", "application/x-troff-ms.ms", "video/x-sgi-movie.mv", "audio/make.my", "application/x-vnd.audioexplosion.mzz.mzz", "image/naplps.nap", "image/naplps.naplps", "application/x-netcdf.nc", "application/vnd.nokia.configuration-message.ncm", "image/x-niff.nif", "image/x-niff.niff", "application/x-mix-transfer.nix", "application/x-conference.nsc", "application/x-navidoc.nvd", "application/octet-stream.o", "application/oda.oda", "application/x-omc.omc", "application/x-omcdatamaker.omcd", "application/x-omcregerator.omcr", "text/x-pascal.p", "application/pkcs10.p10", "application/x-pkcs10.p10", "application/pkcs-12.p12", "application/x-pkcs12.p12", "application/x-pkcs7-signature.p7a", "application/pkcs7-mime.p7c", "application/x-pkcs7-mime.p7c", "application/pkcs7-mime.p7m", "application/x-pkcs7-mime.p7m", "application/x-pkcs7-certreqresp.p7r", "application/pkcs7-signature.p7s", "application/pro_eng.part", "text/pascal.pas", "image/x-portable-bitmap.pbm", "application/vnd.hp-pcl.pcl", "application/x-pcl.pcl", "image/x-pict.pct", "image/x-pcx.pcx", "chemical/x-pdb.pdb", "application/pdf.pdf", "audio/make.pfunk", "audio/make.my.funk.pfunk", "image/x-portable-graymap.pgm", "image/x-portable-greymap.pgm", "image/pict.pic", "image/pict.pict", "application/x-newton-compatible-pkg.pkg", "application/vnd.ms-pki.pko.pko", "text/plain.pl", "text/x-script.perl.pl", "application/x-pixclscript.plx", "image/x-xpixmap.pm", "text/x-script.perl-module.pm", "application/x-pagemaker.pm4", "application/x-pagemaker.pm5", "image/png.png", "application/x-portable-anymap.pnm", "image/x-portable-anymap.pnm", "application/mspowerpoint.pot", "application/vnd.ms-powerpoint.pot", "model/x-pov.pov", "application/vnd.ms-powerpoint.ppa", "image/x-portable-pixmap.ppm", "application/mspowerpoint.pps", "application/vnd.ms-powerpoint.pps", "application/mspowerpoint.ppt", "application/powerpoint.ppt", "application/vnd.ms-powerpoint.ppt", "application/x-mspowerpoint.ppt", "application/mspowerpoint.ppz", "application/x-freelance.pre", "application/pro_eng.prt", "application/postscript.ps", "application/octet-stream.psd", "paleovu/x-pv.pvu", "application/vnd.ms-powerpoint.pwz", "text/x-script.phyton.py", "applicaiton/x-bytecode.python.pyc", "audio/vnd.qcelp.qcp", "x-world/x-3dmf.qd3", "x-world/x-3dmf.qd3d", "image/x-quicktime.qif", "video/quicktime.qt", "video/x-qtc.qtc", "image/x-quicktime.qti", "image/x-quicktime.qtif", "audio/x-pn-realaudio.ra", "audio/x-pn-realaudio-plugin.ra", "audio/x-realaudio.ra", "audio/x-pn-realaudio.ram", "application/x-cmu-raster.ras", "image/cmu-raster.ras", "image/x-cmu-raster.ras", "image/cmu-raster.rast", "text/x-script.rexx.rexx", "image/vnd.rn-realflash.rf", "image/x-rgb.rgb", "application/vnd.rn-realmedia.rm", "audio/x-pn-realaudio.rm", "audio/mid.rmi", "audio/x-pn-realaudio.rmm", "audio/x-pn-realaudio.rmp", "audio/x-pn-realaudio-plugin.rmp", "application/ringing-tones.rng", "application/vnd.nokia.ringing-tone.rng", "application/vnd.rn-realplayer.rnx", "application/x-troff.roff", "image/vnd.rn-realpix.rp", "audio/x-pn-realaudio-plugin.rpm", "text/richtext.rt", "text/vnd.rn-realtext.rt", "application/rtf.rtf", "application/x-rtf.rtf", "text/richtext.rtf", "application/rtf.rtx", "text/richtext.rtx", "video/vnd.rn-realvideo.rv", "text/x-asm.s", "audio/s3m.s3m", "application/octet-stream.saveme", "application/x-tbook.sbk", "application/x-lotusscreencam.scm", "text/x-script.guile.scm", "text/x-script.scheme.scm", "video/x-scm.scm", "text/plain.sdml", "application/sdp.sdp", "application/x-sdp.sdp", "application/sounder.sdr", "application/sea.sea", "application/x-sea.sea", "application/set.set", "text/sgml.sgm", "text/x-sgml.sgm", "text/sgml.sgml", "text/x-sgml.sgml", "application/x-bsh.sh", "application/x-sh.sh", "application/x-shar.sh", "text/x-script.sh.sh", "application/x-bsh.shar", "application/x-shar.shar", "text/html.shtml", "text/x-server-parsed-html.shtml", "audio/x-psid.sid", "application/x-sit.sit", "application/x-stuffit.sit", "application/x-koan.skd", "application/x-koan.skm", "application/x-koan.skp", "application/x-koan.skt", "application/x-seelogo.sl", "application/smil.smi", "application/smil.smil", "audio/basic.snd", "audio/x-adpcm.snd", "application/solids.sol", "application/x-pkcs7-certificates.spc", "text/x-speech.spc", "application/futuresplash.spl", "application/x-sprite.spr", "application/x-sprite.sprite", "application/x-wais-source.src", "text/x-server-parsed-html.ssi", "application/streamingmedia.ssm", "application/vnd.ms-pki.certstore.sst", "application/step.step", "application/sla.stl", "application/vnd.ms-pki.stl.stl", "application/x-navistyle.stl", "application/step.stp", "application/x-sv4cpio.sv4cpio", "application/x-sv4crc.sv4crc", "image/vnd.dwg.svf", "image/x-dwg.svf", "application/x-world.svr", "x-world/x-svr.svr", "application/x-shockwave-flash.swf", "application/x-troff.t", "text/x-speech.talk", "application/x-tar.tar", "application/toolbook.tbk", "application/x-tbook.tbk", "application/x-tcl.tcl", "text/x-script.tcl.tcl", "text/x-script.tcsh.tcsh", "application/x-tex.tex", "application/x-texinfo.texi", "application/x-texinfo.texinfo", "application/plain.text", "text/plain.text", "application/gnutar.tgz", "application/x-compressed.tgz", "image/tiff.tif", "image/x-tiff.tif", "image/tiff.tiff", "image/x-tiff.tiff", "application/x-troff.tr", "audio/tsp-audio.tsi", "application/dsptype.tsp", "audio/tsplayer.tsp", "text/tab-separated-values.tsv", "image/florian.turbot", "text/plain.txt", "text/x-uil.uil", "text/uri-list.uni", "text/uri-list.unis", "application/i-deas.unv", "text/uri-list.uri", "text/uri-list.uris", "application/x-ustar.ustar", "multipart/x-ustar.ustar", "application/octet-stream.uu", "text/x-uuencode.uu", "text/x-uuencode.uue", "application/x-cdlink.vcd", "text/x-vcalendar.vcs", "application/vda.vda", "video/vdo.vdo", "application/groupwise.vew", "video/vivo.viv", "video/vnd.vivo.viv", "video/vivo.vivo", "video/vnd.vivo.vivo", "application/vocaltec-media-desc.vmd", "application/vocaltec-media-file.vmf", "audio/voc.voc", "audio/x-voc.voc", "video/vosaic.vos", "audio/voxware.vox", "audio/x-twinvq-plugin.vqe", "audio/x-twinvq.vqf", "audio/x-twinvq-plugin.vql", "application/x-vrml.vrml", "model/vrml.vrml", "x-world/x-vrml.vrml", "x-world/x-vrt.vrt", "application/x-visio.vsd", "application/x-visio.vst", "application/x-visio.vsw", "application/wordperfect6.0.w60", "application/wordperfect6.1.w61", "application/msword.w6w", "audio/wav.wav", "audio/x-wav.wav", "application/x-qpro.wb1", "image/vnd.wap.wbmp.wbmp", "application/vnd.xara.web", "application/msword.wiz", "application/x-123.wk1", "windows/metafile.wmf", "text/vnd.wap.wml.wml", "application/vnd.wap.wmlc.wmlc", "text/vnd.wap.wmlscript.wmls", "application/vnd.wap.wmlscriptc.wmlsc", "application/msword.word", "application/wordperfect.wp", "application/wordperfect.wp5", "application/wordperfect6.0.wp5", "application/wordperfect.wp6", "application/wordperfect.wpd", "application/x-wpwin.wpd", "application/x-lotus.wq1", "application/mswrite.wri", "application/x-wri.wri", "application/x-world.wrl", "model/vrml.wrl", "x-world/x-vrml.wrl", "model/vrml.wrz", "x-world/x-vrml.wrz", "text/scriplet.wsc", "application/x-wais-source.wsrc", "application/x-wintalk.wtk", "image/x-xbitmap.xbm", "image/x-xbm.xbm", "image/xbm.xbm", "video/x-amt-demorun.xdr", "xgl/drawing.xgz", "image/vnd.xiff.xif", "application/excel.xl", "application/excel.xla", "application/x-excel.xla", "application/x-msexcel.xla", "application/excel.xlb", "application/vnd.ms-excel.xlb", "application/x-excel.xlb", "application/excel.xlc", "application/vnd.ms-excel.xlc", "application/x-excel.xlc", "application/excel.xld", "application/x-excel.xld", "application/excel.xlk", "application/x-excel.xlk", "application/excel.xll", "application/vnd.ms-excel.xll", "application/x-excel.xll", "application/excel.xlm", "application/vnd.ms-excel.xlm", "application/x-excel.xlm", "application/excel.xls", "application/vnd.ms-excel.xls", "application/x-excel.xls", "application/x-msexcel.xls", "application/excel.xlt", "application/x-excel.xlt", "application/excel.xlv", "application/x-excel.xlv", "application/excel.xlw", "application/vnd.ms-excel.xlw", "application/x-excel.xlw", "application/x-msexcel.xlw", "audio/xm.xm", "application/xml.xml", "text/xml.xml", "xgl/movie.xmz", "application/x-vnd.ls-xpix.xpix", "image/x-xpixmap.xpm", "image/xpm.xpm", "image/png.x-png", "video/x-amt-showrun.xsr", "image/x-xwd.xwd", "image/x-xwindowdump.xwd", "chemical/x-pdb.xyz", "application/x-compress.z", "application/x-compressed.z", "application/x-compressed.zip", "application/x-zip-compressed.zip", "application/zip.zip", "multipart/x-zip.zip", "application/octet-stream.zoo", "text/x-script.zsh.zsh" };

        //  #endregion

        //  WebClient client = new WebClient();
        //  //rd.ExportToDisk(ExportFormatType.PortableDocFormat, Session["Orden"].ToString());
        //  string nombre = "ReporteProgreso" + ".pdf";
        //  //rd.ExportToDisk(ExportFormatType.PortableDocFormat, @"D:\ECOMMERCE\SITSADOCADJ\" + Session["index"].ToString() + ".pdf");
        //  rd.ExportToDisk(ExportFormatType.PortableDocFormat, @"C:\Proyectos\ReporteProgreso.pdf");
        //  //string ruta = @"D:\ECOMMERCE\SITSADOCADJ\" + Session["index"].ToString() + ".pdf";
        //  string ruta = @"C:\Proyectos\" + "Reporte Progreso Proyecto" + ".pdf";
        //  Byte[] buffer1 = client.DownloadData(ruta);

        //  //System.Diagnostics.Process proc = new System.Diagnostics.Process();
        //  //proc.StartInfo.FileName = ruta;
        //  //proc.Start();
        //  //proc.Close();

        //  // Byte[] buffer1 = solicitudLogica.seleccionarImagen(id, solicitud);
        //  String[] extArchivo = nombre.Split('.');

        //  foreach (String exten in extensiones)
        //  {
        //    String[] ext = exten.Split('.');

        //    if (ext[1].Equals(extArchivo[extArchivo.Length - 1]))
        //    {
        //      Response.ContentType = ext[0];
        //    }
        //  }
        //  /* string mensaje = "Mensaje";
        //   string[] lista = new string[1];
        //   lista[0] = "IMG_03082015_123738.png";*/
        //  /* Response.Buffer = true;

        //   Response.AddHeader("content-length", buffer1.Length.ToString());
        //   Response.BinaryWrite(buffer1);*/
        //  Response.AddHeader("Content-Disposition", "attachment; filename=" + nombre);
        //  Response.AddHeader("Content-Length", buffer1.Length.ToString());
        //  // Response.ContentType = ReturnExtension(file.Extension.ToLower());
        //  //Response.TransmitFile(ruta);
        //  Response.BinaryWrite(buffer1);
        //  //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "redireccionar();", true);
        //  rd.Close();
        //  rd.Dispose();

        //  Response.End();


        //  /*int fin = rd.FormatEngine.GetLastPageNumber(new CrystalDecisions.Shared.ReportPageRequestContext());
        //  rd.PrintToPrinter(1, false, 1, fin);  //Imprimo 1 copia, de la hoja 1 a la 1*/


        //}
        //Versión ZFA
        //if (e.CommandName == "GenerarPDF")
        //{
        //  ReportDocument rd = new ReportDocument();
        //  rd.Load(Path.Combine(Server.MapPath("~/Reportes"), "ReportePorcentajeProyecto.rpt"));

        //  TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        //  TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        //  ConnectionInfo crConnectionInfo = new ConnectionInfo();
        //  Tables CrTables;

        //  Conexion con = new Conexion();


        //  crConnectionInfo.ServerName = con.ObtenerDato(0); // "Exactus";
        //  crConnectionInfo.DatabaseName = con.ObtenerDato(1);  //"Exactus";
        //  crConnectionInfo.UserID = con.ObtenerDato(2);  //"ECOMMERCE";  
        //  crConnectionInfo.Password = con.ObtenerDato(3); //"Sa123";
        //  crConnectionInfo.Type = ConnectionInfoType.SQL;

        //  crConnectionInfo.IntegratedSecurity = false;

        //  CrTables = rd.Database.Tables;

        //  foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
        //  {
        //    crtableLogoninfo = CrTable.LogOnInfo;
        //    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
        //    CrTable.ApplyLogOnInfo(crtableLogoninfo);
        //    CrTable.Location = "dbo" + "." + CrTable.Location;
        //  }

        //  rd.Refresh();
        //  rd.SetParameterValue("@P_USUARIO", "hcalvo");
        //  rd.SetParameterValue("@P_OPCION", "0");
        //  rd.SetParameterValue("@P_ESQUEMA", "DBO");
        //  PrintDocument printDocument = new PrintDocument();
        //  string Impresora_Predeterminada = printDocument.PrinterSettings.PrinterName;
        //  rd.PrintOptions.PrinterName = Impresora_Predeterminada;
        //  //Opcion para imprimir
        //  rd.PrintToPrinter(1, false, 0, 0);


        //}


      }
      catch (Exception ex)
      {
        Mensaje("Error: ", ex.Message, true);
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "$('#myModalError').modal();", true);
      }
    }

    protected void btnBuscarTiempo_Click(object sender, EventArgs e)
    {

    }

    protected void gvTiempo_PreRender(object sender, EventArgs e)
    {

    }

    protected void gvTiempo_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void gvCargaTrabajo_PreRender(object sender, EventArgs e)
    {

    }

    protected void gvCargaTrabajo_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void btnBuscarCargaTrabajo_Click(object sender, EventArgs e)
    {

    }
    private void Mensaje(string titulo, string msg, bool esCorrecto, string textoBoton = "Ok")
    {
      string icono = esCorrecto ? "success" : "error";
      string script = "Swal.fire({ title: '" + titulo + "!', text: '" + msg + "', icon: '" + icono + "', confirmButtonText: '" + textoBoton + "' })";
      ScriptManager.RegisterStartupScript(this, GetType(), "script", script, true);
    }
  }
}