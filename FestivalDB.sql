
CREATE ROLE app_server_festival2022 WITH
  LOGIN
  NOSUPERUSER
  INHERIT
  NOCREATEDB
  NOCREATEROLE
  NOREPLICATION;

  CREATE ROLE udvikler WITH
  NOLOGIN
  NOSUPERUSER
  INHERIT
  NOCREATEDB
  NOCREATEROLE
  NOREPLICATION;

  CREATE ROLE adminbruger WITH
  LOGIN
  NOSUPERUSER
  INHERIT
  CREATEDB
  CREATEROLE
  NOREPLICATION;

CREATE TABLE IF NOT EXISTS public.roller
(
    rolle_id serial,
    rolle_navn text COLLATE pg_catalog."default" NOT NULL,
    rolle_beskrivelse text COLLATE pg_catalog."default",
    CONSTRAINT roller_pkey PRIMARY KEY (rolle_id)
)

TABLESPACE pg_default;

ALTER TABLE public.roller
    OWNER to adminbruger;

GRANT SELECT ON TABLE public.roller TO app_server_festival2022;

GRANT DELETE, INSERT, SELECT, UPDATE ON TABLE public.roller TO udvikler;

GRANT ALL ON TABLE public.roller TO adminbruger;


CREATE TABLE IF NOT EXISTS public.personer
(
    person_id serial,
    rolle_id integer,
    CONSTRAINT personer_pkey PRIMARY KEY (person_id),
    CONSTRAINT personer_rolle_id_fkey FOREIGN KEY (rolle_id)
        REFERENCES public.roller (rolle_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE public.personer
    OWNER to adminbruger;

GRANT DELETE, INSERT, SELECT, UPDATE ON TABLE public.personer TO udvikler;

GRANT DELETE, INSERT, SELECT, UPDATE ON TABLE public.personer TO app_server_festival2022;

GRANT ALL ON TABLE public.personer TO adminbruger;


CREATE TABLE IF NOT EXISTS public.personlige_oplysninger
(
    email text COLLATE pg_catalog."default" NOT NULL,
    person_id integer,
    telefon text COLLATE pg_catalog."default" NOT NULL,
    kodeord text COLLATE pg_catalog."default" NOT NULL,
    fornavn text COLLATE pg_catalog."default" NOT NULL,
    efternavn text COLLATE pg_catalog."default" NOT NULL,
    "fødselsdag" timestamp with time zone,
    CONSTRAINT personlige_oplysninger_pkey PRIMARY KEY (email),
    CONSTRAINT personlige_oplysninger_person_id_fkey FOREIGN KEY (person_id)
        REFERENCES public.personer (person_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE public.personlige_oplysninger
    OWNER to adminbruger;

GRANT DELETE, INSERT, SELECT, UPDATE ON TABLE public.personlige_oplysninger TO udvikler;

GRANT DELETE, INSERT, SELECT, UPDATE ON TABLE public.personlige_oplysninger TO app_server_festival2022;

GRANT ALL ON TABLE public.personlige_oplysninger TO adminbruger;


CREATE TABLE IF NOT EXISTS public.kompetencer
(
    kompetence_id serial,
    kompetence_navn text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT kompetencer_pkey PRIMARY KEY (kompetence_id)
)

TABLESPACE pg_default;

ALTER TABLE public.kompetencer
    OWNER to adminbruger;

GRANT SELECT ON TABLE public.kompetencer TO app_server_festival2022;

GRANT DELETE, INSERT, SELECT, UPDATE ON TABLE public.kompetencer TO udvikler;

GRANT ALL ON TABLE public.kompetencer TO adminbruger;


CREATE TABLE IF NOT EXISTS public.perskomp
(
    kompetence_id integer NOT NULL,
    person_id integer NOT NULL,
    CONSTRAINT perskomp_kompetence_id_fkey FOREIGN KEY (kompetence_id)
        REFERENCES public.kompetencer (kompetence_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT perskomp_person_id_fkey FOREIGN KEY (person_id)
        REFERENCES public.personer (person_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE public.perskomp
    OWNER to adminbruger;

GRANT DELETE, INSERT, SELECT, UPDATE ON TABLE public.perskomp TO udvikler;

GRANT DELETE, INSERT, SELECT, UPDATE ON TABLE public.perskomp TO app_server_festival2022;

GRANT ALL ON TABLE public.perskomp TO adminbruger;


CREATE TABLE IF NOT EXISTS public.status
(
    status_id serial,
    status_navn text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT status_pkey PRIMARY KEY (status_id)
)

TABLESPACE pg_default;

ALTER TABLE public.status
    OWNER to adminbruger;

GRANT SELECT ON TABLE public.status TO app_server_festival2022;

GRANT ALL ON TABLE public.status TO adminbruger;


CREATE TABLE IF NOT EXISTS public.vagt_typer
(
    vagt_type_id serial,
    vagt_type_navn text COLLATE pg_catalog."default" NOT NULL,
    vagt_type_beskrivelse text COLLATE pg_catalog."default",
    "vagt_type_område" text COLLATE pg_catalog."default" NOT NULL,
    status_id integer NOT NULL,
    CONSTRAINT vagt_typer_pkey PRIMARY KEY (vagt_type_id)
)

TABLESPACE pg_default;

ALTER TABLE public.vagt_typer
    OWNER to adminbruger;

GRANT DELETE, INSERT, SELECT, UPDATE ON TABLE public.vagt_typer TO udvikler;

GRANT DELETE, INSERT, SELECT, UPDATE ON TABLE public.vagt_typer TO app_server_festival2022;

GRANT ALL ON TABLE public.vagt_typer TO adminbruger;


CREATE TABLE IF NOT EXISTS public.vagter
(
    vagt_id serial,
    vagt_type_id integer,
    start_tid timestamp with time zone NOT NULL,
    slut_tid timestamp with time zone NOT NULL,
    person_id integer,
    CONSTRAINT vagter_pkey PRIMARY KEY (vagt_id),
    CONSTRAINT vagter_person_id_fkey FOREIGN KEY (person_id)
        REFERENCES public.personer (person_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT vagter_vagt_type_id_fkey FOREIGN KEY (vagt_type_id)
        REFERENCES public.vagt_typer (vagt_type_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE public.vagter
    OWNER to adminbruger;

GRANT DELETE, INSERT, SELECT, UPDATE ON TABLE public.vagter TO udvikler;

GRANT DELETE, INSERT, SELECT, UPDATE ON TABLE public.vagter TO app_server_festival2022;

GRANT ALL ON TABLE public.vagter TO adminbruger;



CREATE OR REPLACE VIEW public.fuld_person_view_3
 AS
 SELECT array_agg(pk.kompetence_id) AS kompetence_id,
    array_agg(ko.kompetence_navn) AS kompetence_navn,
    pe.person_id,
    pe.rolle_id,
    po.email,
    po.telefon,
    po.kodeord,
    po.fornavn,
    po.efternavn,
    po."fødselsdag"
   FROM personer pe
     JOIN personlige_oplysninger po USING (person_id)
     JOIN perskomp pk USING (person_id)
     JOIN kompetencer ko USING (kompetence_id)
  GROUP BY pe.person_id, pe.rolle_id, po.email, po.telefon, po.kodeord, po.fornavn, po.efternavn, po."fødselsdag";

ALTER TABLE public.fuld_person_view_3
    OWNER TO adminbruger;

GRANT SELECT ON TABLE public.fuld_person_view_3 TO app_server_festival2022;
GRANT ALL ON TABLE public.fuld_person_view_3 TO adminbruger;

CREATE OR REPLACE VIEW public.fuld_vagt_view
 AS
 SELECT v.vagt_id,
    v.vagt_type_id,
    v.start_tid,
    v.slut_tid,
    v.person_id,
    o.vagt_type_navn,
    o.vagt_type_beskrivelse,
    o."vagt_type_område"
   FROM vagter v
     JOIN vagt_typer o ON v.vagt_type_id = o.vagt_type_id;

ALTER TABLE public.fuld_vagt_view
    OWNER TO adminbruger;

GRANT SELECT ON TABLE public.fuld_vagt_view TO app_server_festival2022;
GRANT ALL ON TABLE public.fuld_vagt_view TO adminbruger;


CREATE OR REPLACE PROCEDURE public.opdater_person(
	kompetence_id integer[],
	pe_id integer,
	r_id integer,
	e_mail text,
	tel text,
	kode text,
	forn text,
	efter text,
	"føds" timestamp with time zone)
LANGUAGE 'plpgsql'
AS $BODY$
declare
komp int;
begin
UPDATE personer SET rolle_id = r_id WHERE person_id = pe_id;
UPDATE personlige_oplysninger SET telefon = tel, kodeord = crypt(kode, gen_salt('md5')), fornavn = forn, efternavn = efter, fødselsdag = føds WHERE person_id = pe_id;
DELETE FROM perskomp WHERE person_id = pe_id;
foreach komp IN ARRAY $1
loop
insert into perskomp(person_id, kompetence_id) values (pe_id, komp);
end loop;
end;
$BODY$;

CREATE OR REPLACE PROCEDURE public.opret_person(
	kompetence_id integer[],
	pe_id integer,
	r_id integer,
	e_mail text,
	tel text,
	kode text,
	forn text,
	efter text,
	"føds" timestamp with time zone)
LANGUAGE 'plpgsql'
AS $BODY$
declare
p_id int;
komp int;
begin
insert into personer(rolle_id) values (r_id) returning person_id into p_id;
insert into personlige_oplysninger (person_id, email, telefon, kodeord, fornavn, efternavn, fødselsdag) values(p_id, $4, $5, crypt($6,gen_salt('md5')), $7, $8, $9);
foreach komp IN ARRAY $1
loop
insert into perskomp(person_id, kompetence_id) values (p_id, komp);
end loop;
end;
$BODY$;

GRANT USAGE ON SCHEMA public TO "app_server_festival2022";
GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA public to "app_server_festival2022"
