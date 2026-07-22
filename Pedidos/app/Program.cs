using app.global.global;
using app.main;
using app.serivice.json;

System.Console.WriteLine("\n            ==================== SISTEMA DE GESTION DE PEDIDOS ====================\n");

Global.productos = Json.CargarProductos();
Global.pedidos = Json.CargarPedidos();

Main.Menu();
