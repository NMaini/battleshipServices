# battleshipServices
A WebAPI project. Hosts three endpoints for supporting Battleship board game. 

Endpoint Details: 
1. POST api/board
  - Used to Add a Board and therefore start a new game. Returns a gameId to be used in subsequent calls for adding a ship and attack.
  
2. POST api/ship
  - Used to add a ship. Send start and end co-ordinates as part of request body. GameId return as part of POST api/board should be sent as a header ` { "gameId": "1" } `
  
3. DELETE api/ship
  - Used to attack. Send co-ordinates as part of request body. GameId return as part of POST api/board should be sent as a header ` { "gameId": "1" } `
