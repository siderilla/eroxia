TASK =/= PROMISE

I task si comportano come i promise

Task.wait = function (task) {
  return new Promise((resolve, reject) => {
	task.then(resolve).catch(reject);
  });
}; 

puoi attendere un task come se fosse una promise, ma non puoi usare await su un task e scegliamo noi quando risolvere il task / a che livello fermare la sincronia


overload in c# significa che posso fare più funzioni con lo stesso nome ma con parametri diversi (in javascrit non è permesso)

            //.Result è il modo più brutale di ottenere il risultato di un task sincrono, ma in questo caso è accettabile per semplicità
            //.ContinueWith() è un modo più elegante per ottenere il risultato di un task asincrono, ma richiede una gestione più complessa tipo .then
            //.Wait() è un altro modo per ottenere il risultato di un task sincrono, ma blocca il thread corrente fino al completamento del task

===================================================================================================

COMPITI

1) chiamare client senza employee OK
2) chiamare client con oggetto employee che metteremo dentro il costruttore di client e quindi
   GRAZIE al fiscalcode posso visualizzare in console l'employee associato al client

---------------------------------------------------------------------------------------------------

3) fatto il client, tirare su i purchase product con dentro i loro product
4) tirare su i purchase 
5) spostare tutte le chiamate al db nel momento in cui viene costruito il DBStorage e non più nei costruttori dei vari oggetti
6) smazzatevelo oggi se no morite
7) implementare lato TUI e flusso le richieste del vecchino:
    - nome employee con più guadagni
    - nome prodotto più venduto

===================================================================================================
===================================================================================================

1. ILogic + BusinessLogic
Rappresentano la logica di business, ovvero la parte che:
    -fa da ponte tra la UI (TUI) e i dati (DBStorage),
    -gestisce i dati in memoria, quindi non fa solo pass-through ma mantiene stato (caching, filtraggio, ecc.),
    -decide cosa farne dei dati ricevuti dallo storage: ad esempio, associa un Employee a un Client trovando la corrispondenza con FiscalCodeEmployee.

Riassunto ruoli:
    -ILogic: è solo l’interfaccia (contratto).
    -BusinessLogic: implementa la logica vera. Chiede dati allo storage solo quando servono (lazy loading), li rielabora e li fornisce alla TUI.

2. IStorage + DBStorage
Responsabilità:
    -Sono la parte che parla con il database PostgreSQL.
    -Ogni metodo è una query diretta.
    -Non contengono alcuna logica: restituiscono dati così come sono dal DB (raw → oggetti C#).

Riassunto ruoli:
    -IStorage: interfaccia.
    -DBStorage: implementazione concreta, con query SQL, apertura connessione, ecc.

3. Tui
Responsabilità:
    -È l'interfaccia utente testuale, il menù che vedi in console.
    -Mostra le opzioni, prende input da tastiera, chiama BusinessLogic per eseguire le azioni.
    -NON fa query dirette, NON accede allo storage. Parla solo con ILogic.

---------------------------------------------------------------------------------------------------

FLUSSO:
la tui chiama ilogic che fa da ponte con businesslogic che chiama istorage che fa da ponte a dbstorage
i ponti servono ad ASTRARRE e rendere meno fragile il codice == descrivere cosa VUOI fare, senza specificare il COME (che è nella business/dbstorage)
                    //!!! Interfaccia == Astrazione !!!



