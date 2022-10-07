/// <reference path="datatables.js" />

//Initialize DataTable with function
var InitializeDataTable = function () {
  $('#tableMantenimiento').DataTable({
    "language": {
      aria:
      {
        sortAscending: ": Mostrando datos de forma asencendente",
        sortDescending: ": Mostrando datos de forma desencendente"
      },
      emptyTable: "No hay datos que mostrar",
      info: "Mostrando _START_ a _END_ de _TOTAL_ registros",
      infoEmpty: "No se encontraron registros",
      infoFiltered: "(filtrando de _MAX_ registros)",
      lengthMenu: "Mostrar _MENU_",
      search: "Buscar:",
      zeroRecords: "Ningún registro coincide",
      paginate: {
        previous: "Anterior",
        next: "Siguiente",
        last: "Ultimo",
        first: "Primero"
      },
      buttons: {
        colvis: 'Columnas'
      }
    },
    destroy: true,
    columnDefs: [
      {
        searchable: false, orderable: false, targets: "No-Ordenable"
      }
    ],
    dom: 'Bfrtip',
    buttons: [
      //{
      //  extend: 'print',
      //  exportOptions: {
      //    columns: ':visible'
      //  }
      //},
      //{
      //  extend: 'pdfHtml5',
      //  exportOptions: {
      //    columns: ':visible'
      //  }
      //},
      //{
      //  extend: 'excelHtml5',
      //  exportOptions: {
      //    columns: ':visible'
      //  }
      //},
      //'colvis'
    ],
    responsive: false,
    scrollX: true,
    pageLength: 5
  });
};

function InicialiazaTabla(ID) {
  $("#" + ID).DataTable({
    language: {
      aria:
      {
        sortAscending: ": Mostrando datos de forma asencendente",
        sortDescending: ": Mostrando datos de forma desencendente"
      },
      emptyTable: "No hay datos que mostrar",
      info: "Mostrando _START_ a _END_ de _TOTAL_ registros",
      infoEmpty: "No se encontraron registros",
      infoFiltered: "(filtrando de _MAX_ registros)",
      lengthMenu: "Mostrar _MENU_",
      search: "Buscar:",
      zeroRecords: "Ningún registro coincide"
    },
    "columnDefs": [
      {
        name: "Acciones", searchable: false, orderable: false, targets: "No-Ordenable",
      }
    ],
    PaginationType: "bootstrap",
    paginate: {
      previous: "Ant",
      next: "Sig",
      last: "Ultimo",
      first: "Primero"
    },
    stateSave: true,
    buttons: [],
    responsive: false,
    destroy: true,
    //scrollX: true,
    order: [
      [0, "desc"]
    ],
    lengthMenu: [
      [5, 10, 15, 20, -1],
      [5, 10, 15, 20, "Todos"]
    ],
    pageLength: 5,
    dom: "<'row be-datatable-header'<'col-sm-3'l><'col-sm-4'B><'col-sm-5'f>><'row be-datatable-body'<'col-sm-12'tr>><'row be-datatable-footer'<'col-sm-5'i><'col-sm-7'p>>"
  });
}
//Initialize DataTable with function
function InitializeDataTableWithParameter(ID) {
  $("#" + ID).DataTable({
    "language": {
      aria:
      {
        sortAscending: ": Mostrando datos de forma asencendente",
        sortDescending: ": Mostrando datos de forma desencendente"
      },
      emptyTable: "No hay datos que mostrar",
      info: "Mostrando _START_ a _END_ de _TOTAL_ registros",
      infoEmpty: "No se encontraron registros",
      infoFiltered: "(filtrando de _MAX_ registros)",

      search: "Buscar:",
      zeroRecords: "Ningún registro coincide",
      paginate: {
        previous: "Anterior",
        next: "Siguiente",
        last: "Ultimo",
        first: "Primero"
      },
      buttons: {
        colvis: 'Columnas',
        print: 'Imprimir',
        pageLength: {
          _: "Mostrando %d registros",
          '-1': "Todos los registros"
        }
      }
    },
    responsive: true,
    destroy: true,
    //scrollX: false,
    columnDefs: [
      {
        searchable: false, orderable: false, targets: "No-Ordenable"
      }
    ],
    dom: 'Bfrtip',

    lengthMenu: [
      [5, 10, 25, 50, -1],
      ['5 Registros', '10 Registros', '25 Registros', '50 Registros', 'Todos']
    ],
    buttons: [
      {
        extend: 'pageLength',
        exportOptions: {
          columns: ':visible'
        }
      },
      //{
      //  extend: 'print',
      //  exportOptions: {
      //    columns: ':visible'
      //  }
      //},
      //{
      //  extend: 'pdfHtml5',
      //  exportOptions: {
      //    columns: ':visible'
      //  }
      //},
      //{
      //  extend: 'excelHtml5',
      //  exportOptions: {
      //    columns: ':visible'
      //  }
      //},
      //'colvis'
    ],
    pageLength: 5,
      aaSorting: [],
      stateSave: true,
  });
};

//Initialize DataTable sin descargas
function InitializeDataTableWithParameterAll(ID) {
  $("#" + ID).DataTable({
    "language": {
      aria:
      {
        sortAscending: ": Mostrando datos de forma asencendente",
        sortDescending: ": Mostrando datos de forma desencendente"
      },
      emptyTable: "No hay datos que mostrar",
      info: "Mostrando _START_ a _END_ de _TOTAL_ registros",
      infoEmpty: "No se encontraron registros",
      infoFiltered: "(filtrando de _MAX_ registros)",

      search: "Buscar:",
      zeroRecords: "Ningún registro coincide",
      paginate: {
        previous: "Anterior",
        next: "Siguiente",
        last: "Ultimo",
        first: "Primero"
      },
      buttons: {
        colvis: 'Columnas',
        print: 'Imprimir',
        pageLength: {
          _: "Mostrando %d Registros",
          '-1': "Todos los Registros"
        }
      }
    },
    responsive: false,
    destroy: true,
    //scrollX: true,
    columnDefs: [
      {
        searchable: false, orderable: false, targets: "No-Ordenable"
      }
    ],
    dom: 'Bfrtip',

    lengthMenu: [
      [5, 10, 25, 50, -1],
      ['5 Registros', '10 Registros', '25 Registros', '50 Registros', 'Todos']
    ],
    buttons: [
      //{
      //  extend: 'print',
      //  exportOptions: {
      //    columns: ':visible'
      //  }
      //},
      //{
      //  extend: 'pdfHtml5',
      //  exportOptions: {
      //    columns: ':visible'
      //  }
      //},
      //{
      //  extend: 'excelHtml5',
      //  exportOptions: {
      //    columns: ':visible'
      //  }
      //},
      //'colvis'
    ],
    pageLength: -1,
    aaSorting: []
  });
};

function InitializeDataTableWithParameter10Columns(ID) {
  $("#" + ID).DataTable({
    "language": {
      aria:
      {
        sortAscending: ": Mostrando datos de forma asencendente",
        sortDescending: ": Mostrando datos de forma desencendente"
      },
      emptyTable: "No hay datos que mostrar",
      info: "Mostrando _START_ a _END_ de _TOTAL_ registros",
      infoEmpty: "No se encontraron registros",
      infoFiltered: "(filtrando de _MAX_ registros)",

      search: "Buscar:",
      zeroRecords: "Ningún registro coincide",
      paginate: {
        previous: "Anterior",
        next: "Siguiente",
        last: "Ultimo",
        first: "Primero"
      },
      buttons: {
        colvis: 'Columnas',
        print: 'Imprimir',
        pageLength: {
          _: "Mostrando %d Registros",
          '-1': "Todos los Registros"
        }
      }
    },
    responsive: false,
    destroy: true,
    //scrollX: true,
    columnDefs: [
      {
        searchable: false, orderable: false, targets: "No-Ordenable"
      }
    ],
    dom: 'Bfrtip',

    lengthMenu: [
      [5, 10, 25, 50, -1],
      ['5 Registros', '10 Registros', '25 Registros', '50 Registros', 'Todos']
    ],
    buttons: [
      {
        extend: 'pageLength',
        exportOptions: {
          columns: ':visible'
        }
      },
      //{
      //  extend: 'print',
      //  exportOptions: {
      //    columns: ':visible'
      //  }
      //},
      //{
      //  extend: 'pdfHtml5',
      //  exportOptions: {
      //    columns: ':visible'
      //  }
      //},
      //{
      //  extend: 'excelHtml5',
      //  exportOptions: {
      //    columns: ':visible'
      //  }
      //},
      //'colvis'
    ],
    pageLength: 10,
    aaSorting: []
  });
};




function InitializeDataTableWithParameter25Columns(ID) {
  $("#" + ID).DataTable({
    "language": {
      aria:
      {
        sortAscending: ": Mostrando datos de forma asencendente",
        sortDescending: ": Mostrando datos de forma desencendente"
      },
      emptyTable: "No hay datos que mostrar",
      info: "Mostrando _START_ a _END_ de _TOTAL_ registros",
      infoEmpty: "No se encontraron registros",
      infoFiltered: "(filtrando de _MAX_ registros)",

      search: "Buscar:",
      zeroRecords: "Ningún registro coincide",
      paginate: {
        previous: "Anterior",
        next: "Siguiente",
        last: "Ultimo",
        first: "Primero"
      },
      buttons: {
        colvis: 'Columnas',
        print: 'Imprimir',
        pageLength: {
          _: "Mostrando %d Registros",
          '-1': "Todos los Registros"
        }
      }
    },
    responsive: false,
    destroy: true,
    //scrollX: true,
    columnDefs: [
      {
        searchable: false, orderable: false, targets: "No-Ordenable"
      }
    ],
    dom: 'Bfrtip',

    lengthMenu: [
      [5, 10, 25, 50, -1],
      ['5 Registros', '10 Registros', '25 Registros', '50 Registros', 'Todos']
    ],
    buttons: [
      {
        extend: 'pageLength',
        exportOptions: {
          columns: ':visible'
        }
      },
      //{
      //  extend: 'print',
      //  exportOptions: {
      //    columns: ':visible'
      //  }
      //},
      //{
      //  extend: 'pdfHtml5',
      //  exportOptions: {
      //    columns: ':visible'
      //  }
      //},
      //{
      //  extend: 'excelHtml5',
      //  exportOptions: {
      //    columns: ':visible'
      //  }
      //},
      //'colvis'
    ],
    pageLength: 25,
    aaSorting: []
  });
};


//Initialize DataTable sin descargas
function InitializeDataTableWithParameter2(ID) {
  $("#" + ID).DataTable({
    "language": {
      aria:
      {
        sortAscending: ": Mostrando datos de forma asencendente",
        sortDescending: ": Mostrando datos de forma desencendente"
      },
      emptyTable: "No hay datos que mostrar",
      info: "Mostrando _START_ a _END_ de _TOTAL_ registros",
      infoEmpty: "No se encontraron registros",
      infoFiltered: "(filtrando de _MAX_ registros)",

      search: "Buscar:",
      zeroRecords: "Ningún registro coincide",
      paginate: {
        previous: "Anterior",
        next: "Siguiente",
        last: "Ultimo",
        first: "Primero"
      },
      buttons: {
        colvis: 'Columnas',
        print: 'Imprimir',
        pageLength: {
          _: "Mostrando %d Registros",
          '-1': "Todos los Registros"
        }
      }
    },
    responsive: false,
    destroy: true,
    //scrollX: true,
    columnDefs: [
      {
        searchable: false, orderable: false, targets: "No-Ordenable"
      }
    ],
    dom: 'Bfrtip',

    lengthMenu: [
      [5, 10, 25, 50, -1],
      ['5 Registros', '10 Registros', '25 Registros', '50 Registros', 'Todos']
    ],
    buttons: [
      {
        extend: 'pageLength',
        exportOptions: {
          columns: ':visible'
        }
      },
      //'colvis'
    ],
    pageLength: 5
  });
};

//Initialize DataTable sin descargas
function InitializeDataTableWithParameterGroup(ID) {
  var groupColumn = 2;

  $("#" + ID).DataTable({
    "language": {
      aria:
      {
        sortAscending: ": Mostrando datos de forma asencendente",
        sortDescending: ": Mostrando datos de forma desencendente"
      },
      emptyTable: "No hay datos que mostrar",
      info: "Mostrando _START_ a _END_ de _TOTAL_ registros",
      infoEmpty: "No se encontraron registros",
      infoFiltered: "(filtrando de _MAX_ registros)",

      search: "Buscar:",
      zeroRecords: "Ningún registro coincide",
      paginate: {
        previous: "Anterior",
        next: "Siguiente",
        last: "Ultimo",
        first: "Primero"
      },
      buttons: {
        colvis: 'Columnas',
        print: 'Imprimir',
        pageLength: {
          _: "Mostrando %d Registros",
          '-1': "Todos los Registros"
        }
      }
    },
    responsive: false,
    destroy: true,
    //scrollX: true,
    columnDefs: [
      {
        searchable: false, orderable: false, visible: false, targets: groupColumn
      }
    ],
    dom: 'Bfrtip',

    lengthMenu: [
      [5, 10, 25, 50, -1],
      ['5 Registros', '10 Registros', '25 Registros', '50 Registros', 'Todos']
    ],
    buttons: [
      {
        extend: 'pageLength',
        exportOptions: {
          columns: ':visible'
        }
      },
      //'colvis'
    ],
    pageLength: 10,
    "drawCallback": function (settings) {
      var api = this.api();
      var rows = api.rows({ page: 'current' }).nodes();
      var last = null;

      api.column(groupColumn, { page: 'current' }).data().each(function (group, i) {
        if (last !== group) {
          $(rows).eq(i).before(
            '<tr class="group"><td colspan="10">' + group + '</td></tr>'
          );
          last = group;
        }
      });
    }
  });
};

//Initialize DataTable with function
function InitializeDataTableModal(ID) {
  $("#" + ID).DataTable({
    "language": {
      aria:
      {
        sortAscending: ": Mostrando datos de forma asencendente",
        sortDescending: ": Mostrando datos de forma desencendente"
      },
      emptyTable: "No hay datos que mostrar",
      info: "Mostrando _START_ a _END_ de _TOTAL_ registros",
      infoEmpty: "No se encontraron registros",
      infoFiltered: "(filtrando de _MAX_ registros)",
      search: "Buscar:",
      zeroRecords: "Ningún registro coincide",
      paginate: {
        previous: "Anterior",
        next: "Siguiente",
        last: "Ultimo",
        first: "Primero"
      },
      buttons: {
        print: 'Imprimir'

      }
    },
    responsive: false,
    destroy: true,
    //scrollX: true,
    columnDefs: [
      {
        searchable: false, orderable: false, targets: "No-Ordenable"
      }
    ],
    dom: 'Bfrtip',
    buttons: [
      //{
      //  extend: 'print',
      //  exportOptions: {
      //    columns: ':visible'
      //  }
      //},
      //{
      //  extend: 'pdfHtml5',
      //  exportOptions: {
      //    columns: ':visible'
      //  }
      //},
      //{
      //  extend: 'excelHtml5',
      //  exportOptions: {
      //    columns: ':visible'
      //  }
      //}
    ],
    pageLength: 6
  });
}

function InitializeDataTableSinNada(ID) {
  $("#" + ID).DataTable({
    "bFilter": false,
    "language": {
      aria:
      {
        sortAscending: ": Mostrando datos de forma asencendente",
        sortDescending: ": Mostrando datos de forma desencendente"
      },
      emptyTable: "No hay datos que mostrar",
      info: "Mostrando _START_ a _END_ de _TOTAL_ registros",
      infoEmpty: "No se encontraron registros",
      infoFiltered: "(filtrando de _MAX_ registros)",
      search: "Buscar",
      zeroRecords: "Ningún registro coincide",
      paginate: {
        previous: "Anterior",
        next: "Siguiente",
        last: "Ultimo",
        first: "Primero"
      },
      buttons: {
        print: 'Imprimir'

      }
      },
      responsive: false,
      destroy: true,
      scrollX: true,

    //responsive: false,
    //destroy: true,
    ////scrollX: true,
    columnDefs: [
      {
        searchable: false, orderable: false, targets: "No-Ordenable"
      }
    ],
    dom: 'Bfrtip',
    buttons: [
    ],
    pageLength: 3
  });
}

function InitializeDataTableSinBotones(ID) {
  $("#" + ID).DataTable({
    "language": {
      aria:
      {
        sortAscending: ": Mostrando datos de forma asencendente",
        sortDescending: ": Mostrando datos de forma desencendente"
      },
      emptyTable: "No hay datos que mostrar",
      info: "Mostrando _START_ a _END_ de _TOTAL_ registros",
      infoEmpty: "No se encontraron registros",
      infoFiltered: "(filtrando de _MAX_ registros)",
      search: "Buscar",
      zeroRecords: "Ningún registro coincide",
      paginate: {
        previous: "Anterior",
        next: "Siguiente",
        last: "Ultimo",
        first: "Primero"
      },
      buttons: {
        print: 'Imprimir'

      }
    },
    responsive: false,
    destroy: true,
    //scrollX: true,
    columnDefs: [
      {
        searchable: false, orderable: false, targets: "No-Ordenable"
      }
    ],
    dom: 'Bfrtip',
    buttons: [
    ],
    pageLength: 10
  });
}

//Initialize Select2 Elements
var InitializeSelect2 = function () {
  $('.select2').select2({
    language: {
      noResults: function () {
        return "No hay resultados";
      },
      searching: function () {
        return "Buscando..";
      }
    }
  });
};

const InitTableWithParams = (pId, pPageLength = -1, pStateSave = false, pIsResponsive = false) => {
    $("#" + pId).DataTable({
        "language": {
            aria:
            {
                sortAscending: ": Mostrando datos de forma asencendente",
                sortDescending: ": Mostrando datos de forma desencendente"
            },
            emptyTable: "No hay datos que mostrar",
            info: "Mostrando _START_ a _END_ de _TOTAL_ registros",
            infoEmpty: "No se encontraron registros",
            infoFiltered: "(filtrando de _MAX_ registros)",

            search: "Buscar:",
            zeroRecords: "Ningún registro coincide",
            paginate: {
                previous: "Anterior",
                next: "Siguiente",
                last: "Ultimo",
                first: "Primero"
            },
            buttons: {
                colvis: 'Columnas',
                print: 'Imprimir',
                pageLength: {
                    _: "Mostrando %d Registros",
                    '-1': "Todos los Registros"
                }
            }
        },
        responsive: pIsResponsive,
        destroy: true,
        columnDefs: [
            {
                searchable: false, orderable: false, targets: "No-Ordenable"
            }
        ],
        dom: 'Bfrtip',
        lengthMenu: [
            [5, 10, 25, 50, -1],
            ['5 Registros', '10 Registros', '25 Registros', '50 Registros', 'Todos']
        ],
        buttons: [
            {
                extend: 'pageLength',
                exportOptions: {
                    columns: ':visible'
                }
            },
            //{
            //  extend: 'print',
            //  exportOptions: {
            //    columns: ':visible'
            //  }
            //},
            //{
            //  extend: 'pdfHtml5',
            //  exportOptions: {
            //    columns: ':visible'
            //  }
            //},
            //{
            //  extend: 'excelHtml5',
            //  exportOptions: {
            //    columns: ':visible'
            //  }
            //},
            //'colvis'
        ],
        pageLength: pPageLength,
        aaSorting: [],
        stateSave: pStateSave
    });
};
