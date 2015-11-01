# OpenAntrag
Quellcode der Website openantrag.de

Über das Portal OpenAntrag können Bürger über Fraktionen oder Einzelabgeordnete der Piratenpartei ihre Ideen und Wünsche einbringen. 

Das Portal ist realisiert für:

Deutschland (openantrag.de. open-antrag.de)
Das Projekt wurde mit ASP.NET MVC umgesetzt. Als Datenbank kommt RavenDB zum Einsatz.

Um das Projekt lauffähig zu bekommen sind weitere Komponenten bzw. Dateien notwendig:

# RavenDB #

Als Backend kommt die NoSQL-Datenbank **RavenDB** zum Einsatz. Alle Objekte werden automatisch persistiert. Nach Installation von RavenDB und Anlegen einer 'OpenAntrag'-Datenbank ist lediglich die Anlage eines fixen Dokuments notwendig:

    ProposalTags-1
    
    {
     "Items": [],
     "CreatedAt": null,
     "CreatedBy": null,
     "Timestamp": 0
    }

Dieses Dokument nimmt alle verfügbaren Themen (Schlagworte) auf und verknüpft sie mit den Anträgen (Proposals). 

# Dateien #

## web.config ##

Nicht im Source-Code enthalten ist die web.config des Projekts, da sie in den AppSettings Logins und API-Keys enthält. Muster

## Roles.xml / Users.xml ##

Die Authentifizierung basiert auf den ASP.NET Xml Security Providers. Hierzu werden im Ordner \App_Data diese beiden XML-Dateien benötigt.

## representation.xml ##

Alle verfügbaren Parlamente werden in dieser Datei unter \App_Data angelegt, die folgende Struktur hat:

    <?xml version="1.0" encoding="utf-8" ?>
    	<representations>
    		<item id="999" key="testparlament" status="0" api-key="..."
      			  label="Test" color="#999" 
      			  name="Test-Parlament" name2="Parlament (Test)"
      			  level="4" federal="" map-url=""
      			  group-type="1" group-name="Test"
      			  link=""
      			  phone="" twitter=""
      			  mail="test@mail.de"
      			  info-mail="test@mail.de">
				<representatives>
		       		<item id="1" key="hans-mustermann" name="Hans Mustermann"
		     			  mail="" phone="" twitter=""
		     			  twitter=""
		     			  party="Piratenpartei" />
				</representatives>
		       	<committees>
		     		<item id="" key="" caption="" name="" url=""></item>
		       	</committees>
				<process>
		     		<step id="1" def="eingang" 
		       			  caption="Eingang des Antrags"
		       			  short-caption="Antragseingang">
						<next>
		     		<step id="2" />
		       			</next>
		     		</step>
		     		<step id="2" def="pruefung" 
		       			  caption="Antrag in Prüfung" 
		       			  short-caption="Antrag in Prüfung">
		       			<next>
		     		<step id="3" />
		       			</next>
		     		</step>
					...
		     		<step id="..." def="erledigt" success-story="true" 
						  caption="Antrag erledigt" 
		       			  short-caption="Antrag erledigt">
		       			<next />
		     		</step>
       			</process>
       		</item>
    	</representations>
 

|Feld|Bedeutung|
|---|---|
|`item.id`|eindeutige fortlaufende Nummer des Parlaments|
|`item.key`|eindeutiger Schlüssel für das Parlament|
|`item.label`|Name der Region (Stadt, Kreis, Land)|
|`item.color`|individueller Farb-Code|
|`item.name`|Name des Parlaments|
|`item.name2`|Name des Parlaments mit vorangestelltem Namen der Region|
|`item.level`|Regierungsebene (Schlüssel auf GovernmentalLevels.xml)|
|`item.federal`|Bundesland (Schlüssel auf FederalStates.xml)|
|`item.map-url`|Link über http://geojson.io (Daten aus piratenmandate-frontend)|
|`item.group-type`|Gruppentyp (Schlüssel auf GroupTypes.xml)|
|`item.group-name`|Gruppenname|
|`item.link`|Website-Link der Gruppe|
|`item.phone`|Telefonnummer der Gruppe|
|`item.mail`|Mail-Adresse der Gruppe|
|`item.info-mail`|Info-Mail-Adresse der Gruppe (Versand der Benachrichtigungen)|
|`item.representatives.id`|eindeutige fortlaufende Nummer des Mandatsträgers innerhalb des Parlaments|
|`item.representatives.key`|eindeutiger Schlüssel für den Mandatsträger innerhalb des Parlaments|
|`item.representatives.name`|Name des Mandatsträgers|
|`item.representatives.mail`|Mail-Adresse des Mandatsträgers|
|`item.representatives.phone`|Telefonnummer des Mandatsträgers|
|`item.representatives.twitter`|Twitter-Account des Mandatsträgers|
|`item.representatives.party`|Partei des Mandatsträgers|
|`item.committees.id`|eindeutige fortlaufende Nummer des Ausschusses innerhalb des Parlaments|
|`item.committees.key`|eindeutiger Schlüssel für den Ausschuss innerhalb des Parlaments|
|`item.committees.caption`|Kurzname des Ausschusses|
|`item.committees.name`|vollständiger Name des Ausschusses|
|`item.committees.url`|Link zum Ausschuss (Ratssystem)|
|`item.process.step.id`|eindeutige fortlaufende Nummer des Antragsschritts innerhalb des Parlaments|
|`item.process.step.def`|Definion des Antragsschritts (Schlüssel auf ProcessStepDefinitions.xml)|
|`item.process.step.caption`|Kurzname des Antragsschritts (überschreibt den Kurznamen aus der Definition)|
|`item.process.step.short-caption`|Name des Antragsschritts (überschreibt den Namen aus der Definition)|
 
