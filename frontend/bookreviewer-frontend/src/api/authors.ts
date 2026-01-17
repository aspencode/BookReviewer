import { http } from "./http";
import type { AuthorListDto, PaginatedAuthors } from "../models/authors";

export async function getAuthors(): Promise<AuthorListDto[]> {

  const response = await http.get<PaginatedAuthors>("/Authors?pageNumber=1&pageSize=10");
  

  return response.data.items; 
}

export async function createAuthor(name: string): Promise<void> {
  await http.post("/Authors", { name });
}