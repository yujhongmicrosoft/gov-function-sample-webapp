import { TestBed, inject } from '@angular/core/testing';

import { ModeratorApiService } from './moderator-api.service';

describe('ModeratorApiService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ModeratorApiService]
    });
  });

  it('should be created', inject([ModeratorApiService], (service: ModeratorApiService) => {
    expect(service).toBeTruthy();
  }));
});
