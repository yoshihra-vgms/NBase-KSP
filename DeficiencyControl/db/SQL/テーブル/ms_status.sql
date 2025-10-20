-- Table: ms_status

-- DROP TABLE ms_status;

CREATE TABLE ms_status
(
  status_id serial NOT NULL,
  status_name character varying,
  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_ms_status PRIMARY KEY (status_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE ms_status
  OWNER TO "IGTDefconUser";

-- Trigger: tr_ms_status on ms_status

-- DROP TRIGGER tr_ms_status ON ms_status;

CREATE TRIGGER tr_ms_status
  BEFORE INSERT OR UPDATE
  ON ms_status
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();

