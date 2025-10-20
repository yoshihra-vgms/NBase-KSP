-- Table: ms_viq_no

-- DROP TABLE ms_viq_no;

CREATE TABLE ms_viq_no
(

  viq_no_id serial NOT NULL,
  viq_code_id integer,
  viq_no character varying,
  description character varying,
  description_eng character varying,
  order_no integer,



  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_ms_viq_no PRIMARY KEY (viq_no_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE ms_viq_no
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE ms_viq_no
  IS 'VIQ Noマスタ';

-- Trigger: tr_ms_viq_no on ms_viq_no

-- DROP TRIGGER tr_ms_viq_no ON ms_viq_no;

CREATE TRIGGER tr_ms_viq_no
  BEFORE INSERT OR UPDATE
  ON ms_viq_no
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();