-- Table: ms_inspection_category

-- DROP TABLE ms_inspection_category;

CREATE TABLE ms_inspection_category
(
  inspection_category_id serial NOT NULL,
  inspection_category_name character varying,



  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_ms_inspection_category PRIMARY KEY (inspection_category_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE ms_inspection_category
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE ms_inspection_category
  IS '検船種別マスタ';

-- Trigger: tr_ms_inspection_category on ms_inspection_category

-- DROP TRIGGER tr_ms_inspection_category ON ms_inspection_category;

CREATE TRIGGER tr_ms_inspection_category
  BEFORE INSERT OR UPDATE
  ON ms_inspection_category
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();