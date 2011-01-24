CREATE TABLE ModelScenario ( 
	scenarioID Integer NOT NULL,
	stormID Integer,
	description Text(50)
)
;


ALTER TABLE ModelScenario ADD CONSTRAINT PK_ModelScenario 
	PRIMARY KEY (scenarioID)
;


ALTER TABLE ModelScenario
	ADD CONSTRAINT UQ_ModelScenario_scenarioID UNIQUE (scenarioID)
;



ALTER TABLE ModelScenario ADD CONSTRAINT FK_ModelScenario_StormCatalog 
	FOREIGN KEY (stormID) REFERENCES StormCatalog (stormID)
;
