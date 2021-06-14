CREATE TABLE IF NOT EXISTS "public"."item_types" (
  "id" int4 NOT NULL,
  "name" varchar NOT NULL,
  "create_date" timestamp DEFAULT now(),
  PRIMARY KEY ("id")
)
;

INSERT INTO "public"."item_types" ("id", "name") VALUES (1, 'User');
INSERT INTO "public"."item_types" ("id", "name") VALUES (2, 'Post');
INSERT INTO "public"."item_types" ("id", "name") VALUES (3, 'Like');
INSERT INTO "public"."item_types" ("id", "name") VALUES (4, 'Attachment');
INSERT INTO "public"."item_types" ("id", "name") VALUES (5, 'Comment');