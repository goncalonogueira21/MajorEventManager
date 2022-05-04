CREATE TABLE [dbo].[Evento] (
    [Id]        INT           NOT NULL,
    [Descricao] VARCHAR (MAX) NULL,
    [Titulo]    VARCHAR (50)  NULL,
    [Categoria] VARCHAR (50)  NULL,
    [Inicio]    DATETIME      NULL,
    [Fim]       DATETIME      NULL,
    [Latitude]  FLOAT (53)    NULL,
    [Longitude] FLOAT (53)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
