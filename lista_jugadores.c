// Incluir la librería para MySQL
#include <mysql.h>
#include <string.h>
#include <stdlib.h>
#include <stdio.h>
int main(int argc, char **argv)
{
	MYSQL *conn;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	
	// Crear conexión al servidor MySQL
	conn = mysql_init(NULL);
	if (conn == NULL) {
		printf("Error al crear la conexión: %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit(1);
	}
	// Conectar a la base de datos
	conn = mysql_real_connect(conn, "localhost", "root", "mysql", "Play", 0, NULL, 0);
	if (conn == NULL) {
		printf("Error al conectar a la base de datos: %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit(1);
	}
	// Ejecutar consulta para obtener todos los jugadores
	err = mysql_query(conn, "SELECT * FROM Player;");
	if (err != 0) {
		printf("Error al consultar datos de la base: %u %s\n", mysql_errno(conn), mysql_error(conn));
		mysql_close(conn);
		exit(1);
	}
	// Recoger el resultado de la consulta
	resultado = mysql_store_result(conn);
	if (resultado == NULL) {
		printf("Error al almacenar resultado: %u %s\n", mysql_errno(conn), mysql_error(conn));
		mysql_close(conn);
		exit(1);
	}
	// Mostrar cabecera
	printf("Lista de jugadores:\n");
	printf("%-5s %-15s %-10s %-30s\n", "ID", "Username", "Name", "Address");
	
	// Recorrer el resultado e imprimir cada jugador
	while ((row = mysql_fetch_row(resultado))) {
		printf("%-5s %-15s %-10s %-30s\n", row[0], row[1], row[2], row[3]);
	}
	// Liberar memoria y cerrar conexión
	mysql_free_result(resultado);
	mysql_close(conn);
	printf("Error al conectar a la base de datos: %s\n", mysql_error(conn));
	
	
	
	return 0;
}
