Os diferentes cenários do teste foram reproduzidos nos testes unitários

* "Write a class Network. The constructor should take a positive integer value indicating the 
number of elements in the set.  Passing in an invalid value should throw an exception."
![image](https://github.com/user-attachments/assets/d1b97153-38bc-4f1a-a0f0-cf1b75e14550)

* Conectando dois elementos
  
![image](https://github.com/user-attachments/assets/099b3cd1-722b-472d-b2b4-0a84554da0a9)

* Conectando um elemento à um outro elemento que já contém uma conexão.
O elemento terá agora as conexões indiretas pertencendo à mesma coleção que está inserido.
(Nesse exemplo, o elemento 3, após ser conectado ao 2, participa da mesma coleção do elemento 1.
Essa coleção representa uma cadeia de conexões. Qualquer elemento que se conecte com qualquer
um dos elementos pertencentes à essa cadeia integrará a coleção)

![image](https://github.com/user-attachments/assets/0987fab7-7431-4d10-b401-198f0f277f67)

* Conectando dois elementos com conexões já existentes, gerando uma única conexão no final.

![image](https://github.com/user-attachments/assets/0b212e03-e4e5-4cb9-acf9-6ed6c864bcff)

* Consulta (true ou false). Como demonstrado nos exemplos acima, elementos conectados indiretamente
são postos todos em uma única conexão, facilitando a busca.

![image](https://github.com/user-attachments/assets/b7d8b8ff-5225-4c37-ae61-1f37d8f49377)
