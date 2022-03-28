-- Table: public.User

-- DROP TABLE IF EXISTS public."User";

CREATE TABLE IF NOT EXISTS public."User"
(
    "Name" character varying(100) COLLATE pg_catalog."default" NOT NULL,
    "CreatedAt" timestamp without time zone NOT NULL,
    "Id" uuid NOT NULL,
    "DeletedAt" timestamp without time zone,
    "LastUpdateAt" timestamp without time zone
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."User"
    OWNER to postgres;
-- Index: Id_index

-- DROP INDEX IF EXISTS public."Id_index";

CREATE UNIQUE INDEX IF NOT EXISTS "Id_index"
    ON public."User" USING btree
    ("Id" ASC NULLS LAST)
    INCLUDE("Id")
    TABLESPACE pg_default;