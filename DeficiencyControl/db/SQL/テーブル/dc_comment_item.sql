-- Table: dc_comment_item

-- DROP TABLE dc_comment_item;

CREATE TABLE dc_comment_item
(
  comment_item_id serial NOT NULL,
  comment_id integer,
  
  ms_vessel_id numeric(4,0),
  ms_crew_matrix_type_id numeric(4,0),
  
  item_kind_id integer,
  date timestamp without time zone,
  search_keyword text,
  
  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  
  CONSTRAINT pk_dc_comment_item PRIMARY KEY (comment_item_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE dc_comment_item
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE dc_comment_item
  IS 'コメントアイテムテーブル';

-- Trigger: tr_dc_comment_item on dc_comment_item

-- DROP TRIGGER tr_dc_comment_item ON dc_comment_item;

CREATE TRIGGER tr_dc_comment_item
  BEFORE INSERT OR UPDATE
  ON dc_comment_item
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();

