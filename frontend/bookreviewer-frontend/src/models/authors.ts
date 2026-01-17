export interface AuthorListDto {
  id: number;
  name: string;
}


export interface PaginatedAuthors {
  items: AuthorListDto[];
  totalCount: number;
  pageSize: number;
  currentPage: number;
  totalPages: number;
}