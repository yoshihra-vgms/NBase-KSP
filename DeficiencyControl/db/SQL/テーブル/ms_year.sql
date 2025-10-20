-- Table: ms_year

-- DROP TABLE ms_year;

CREATE TABLE ms_year
(
  year_id serial NOT NULL,
  year integer,
  start_date timestamp without time zone,
  end_date timestamp without time zone,
 
  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_ms_year PRIMARY KEY (year_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE ms_year
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE ms_year
  IS '年度マスタ';

-- Trigger: tr_ms_year on ms_year

-- DROP TRIGGER tr_ms_year ON ms_year;

CREATE TRIGGER tr_ms_year
  BEFORE INSERT OR UPDATE
  ON ms_year
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();