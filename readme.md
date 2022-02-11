﻿# Mutations, Batching and Treansactions

This shows example of building, using Mutations, bacthing in query and enabling transactions

slight modifications had to be made the the initial Query class to enable the ability to call multiple queries in the same post message

## Batching

Batching in GraphQL is handled differently then with batching in other batch calls like OData could do queries and edits at the same time if necessary,
GraphQL wants the items to be all the same within one call.  

You can link multiples queries together, but you cannot perform a query and then a mutation in the same call.  This is because the way that mutations are resolved in series versus Queries which are resolved in parallel

To do multiple queries each operation must have a name and the URL has to be modified to achieve it to work so if we name two queries A and B the url would look like the following

 ```html
  https://localhost:7019/graphql?batchOperations=[A,B]
 ```

and the GraphQL query would look like

 ```graphql
  query A {
            books {
                addressId 
                title
                price
          }
        }
        query B {
            addresses {
                street
                city
            }
        }
    ```

### Batching using export to chain queries

Batching helps alleviate the 'chattyness' of GraphQL and allows the queries to be gouped to simplify the work.  The ```@export``` command allows the queries to be linked.  
If we wanted to have the results of one query filter another query we would use @export like so (assuming the same url as before in batching)
    ```graphql
        query A {
            books {
                addressId @export(as: "ids")
                title
                price
          }
        }
        query B {
            addressesById(id: $ids) {
                street
                city
            }
        }
    ```
The AddressIds are exported as a variable ids (lower case is important since it matches the routes)  Note that is now variables are declared in GraphQL, by using the dollar sign More about variables when we reach mutations

## Mutations
Mutations are the way that we edit, add, and delete data.  Generally a mutation required the defined input, the return type.  Hot Chocolate uses a specific naming convention in the input and return type variable naming.  To add a book entry you need to define the input as *xxxxPayLoad*.  For this example I am using the new C# feature of record to make it easier to define the input objects.  
``` C#
public record InputBookPayLoad(string Isbn, string Title, string Author, decimal Price, Guid AddressId, Guid PressId);
public async Task<Book> AddBook(InputBookPayLoad input)
        {
            var context = dbContextFactory.CreateDbContext();
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Isbn = input.Isbn,
                Title= input.Title,
                Author = input.Author,
                Price = input.Price,
                AddressId = input.AddressId,
                PressId = input.PressId
            };

            context.Books.Add(book);
            await context.SaveChangesAsync();
            return book;
        }
```