#include <stdio.h>
#include <string.h>
#include <stdlib.h>

int main(int argc, char *argv[]) {
	
	char peticion[100];
	
	strcpy (peticion, "3/Miguel/47/1.78");
	
	int codigo;
	char nombre[20];
	int edad;
	float altura;
	
	char *p = strtok (peticion, "/");
	codigo = atoi (p); // atoi convierte string en un numero entero (int)
	p = strtok (NULL, "/");
	strcpy (nombre, p);
	p = strtok (NULL, "/");
	edad = atoi (p);
	p = strtok (NULL, "/");
	altura = atof (p); // atof convierte string en un numero decimal (float)
	
	printf ("Codigo: %d, nombre: %s, edad: %d, altura: %f\n", codigo, nombre, edad, altura);
	
	char respuesta[100];
	if (edad > 18)
		// sprintf es como un printf pero imprimiendo en un string en lugar de la consola
		sprintf (respuesta, "%s es mayor de edad porque tiene %d años", nombre, edad);
	else
		sprintf (respuesta, "%s no es mayor de edad", nombre);
		
	printf ("Respuesta: %s\n", respuesta);
	
}

