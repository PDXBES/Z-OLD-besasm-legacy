IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_SICID_FK') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE DocumentSpatialRelations DROP CONSTRAINT FK_SICID_FK
;


IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('DocumentSpatialRelations') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE DocumentSpatialRelations
;

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('SpatialIndexLayerCatalog') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE SpatialIndexLayerCatalog
;




CREATE TABLE DocumentSpatialRelations ( 
	DocRelationID int NOT NULL,
	DocumentURI bigint,
	DocumentURL varchar(500),
	SICID_FK varchar(200),
	SICKeyvalue varchar(200)
)
;

CREATE TABLE SpatialIndexLayerCatalog ( 
	SICID int NOT NULL,
	LayerName varchar(200),
	LayerPath varchar(200),
	LayerTech char(10),
	IndexColumnName varchar(200)
)
;


ALTER TABLE DocumentSpatialRelations ADD CONSTRAINT PK_DocumentSpatialRelations 
	PRIMARY KEY CLUSTERED (DocRelationID)
;

ALTER TABLE SpatialIndexLayerCatalog ADD CONSTRAINT PK_SpatialIndexLayerCatalog 
	PRIMARY KEY CLUSTERED (SICID)
;


ALTER TABLE DocumentSpatialRelations
	ADD CONSTRAINT UQ_DocumentSpatialRelations_DocRelationID UNIQUE (DocRelationID)
;

ALTER TABLE SpatialIndexLayerCatalog
	ADD CONSTRAINT UQ_SpatialIndexLayerCatalog_SICID UNIQUE (SICID)
;
